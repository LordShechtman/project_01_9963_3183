using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
namespace BL
{

    public class Bl_imp : IBL
    {

        #region Singleton
        public static readonly Bl_imp instance = new Bl_imp();

        public static Bl_imp Instance { get { return instance; } } 



        #endregion
        static Idal dal;
        #region Constractor
        private Bl_imp() { }
        static Bl_imp()
        {
            dal = Factory_dal.getDal();
        }

        #endregion
        #region Valid input Logic
        int distance(Address address1, Address address2)
        {
            Random r = new Random();
            ///Meanwhile we wait for permission from Google Maps we will return random distance;
            return r.Next(0, 151);
        }
       public void ValidAddress(Address address)
        {
            //address must contin all it filed
            if (address.city == null)
                throw new Exception("Address must contain city name");
            if (address.streetName == null)
                throw new Exception("Address must contain street name");
            if (address.houseNumber <= 0)
                throw new Exception("Address must contain house number");
        }
        //TODO: use this deleagte
        delegate bool myFunc(List<object> lst, myFunc conditon);
        
        public void ifNonLetters(string str,string name)
        {
            //Check for ID and phone number if there are no letters in the string 
            foreach(char ch in str)
            {
                if (ch < '0' || ch > '9')
                    throw new Exception(name + " must contin dighits only!!");
            }
        }
        public bool TestResult(Test test)
        {
            //Detrminate the reuslt of the test
            int count = test.TestParameters.Count(item => item == true);
            return count / 6 >= 4 && test.TestParameters[5] == true && test.TestParameters[4] == true && test.TestParameters[0] == true;
        }
        #endregion
        #region Methods
        public int numberOfTests(Trainee T)
        {
          /*Using lambda exprssion to find how many tets certin student made*/
            IEnumerable<Test > lst= dal.GetAllTests();
            IEnumerable<Test> count = lst.Where(i => i.TraineeId == T.Id).Select(i=>i);
            return count.Count();
            
        }
        public List<Trainee> passedToday()
        {
            //retuns a list of students who passed Today(   Date Time.now)
            List<Trainee> passed = new List<Trainee>();
            List<Trainee> lst = dal.GetAllTrainees();
            foreach(Test T in dal.GetAllTests())
            {
                if (T.IsPass ==true && (T.TestDate.Day==DateTime.Now.Day && T.TestDate.Month == DateTime.Now.Month && T.TestDate.Year == DateTime.Now.Year))
                {
                    foreach(Trainee trainee in lst)
                    {
                        if(trainee.Id==T.TraineeId)
                        {
                            passed.Add(trainee);
                            break;
                        }
                    }
                }
            }
            return passed;
        }

        public bool isTraineePassed(string id)
        {

            Test passed =  dal.GetAllTests(). Find(item=> item.TraineeId == id && item.IsPass == true);
            return passed != null;
        }
        public List<Tester> avilableTesters(int hour, days day)
        {
           IEnumerable<Tester> able = from item in dal.GetAllTesters()
          where item.WorkHours[(int)day, hour - 9] == true
          select item;
          return able.ToList();
        }
        

        #endregion
        void IBL.AddTest( string trainee_id, Address address ,DateTime date)
        {
           List<Tester> all_my_testers = dal.GetAllTesters();
           List<Trainee> all_my_trainees = dal.GetAllTrainees();
            List<Test> all_my_test = dal.GetAllTests();
            try
            {
                Test temp;
                
                   //// Find tester that can make the test in that data
                   
                if (trainee_id.Length != 9 )
                    throw new Exception("Trainee ID must contin only 9 dights!!");
                
                ifNonLetters(trainee_id, " Trainee ID");
                if (address.city == null)
                    //address must contain all it fileds
                    throw new Exception("Address must contain city name");
                if (address.streetName == null)
                    throw new Exception("Address must contain street name");
                if (address.houseNumber <= 0)
                    throw new Exception("Address must contain house number");
                if (date.Hour > 16 || date.Hour < 9)
                    throw new Exception("Test can only be between 9:00-16:00!!");
                Tester valid_tester;
                
                List<Tester> valid_testers = all_my_testers.FindAll(item => item.WorkHours[(int)date.DayOfWeek - 1, (int)date.Hour -9] == true);
                bool flag = false;
                foreach (Tester t in valid_testers)
                {
                    Test invalid_test = all_my_test.Find(item => item.TestDate.Day == date.Day && item.TestDate.Month == date.Month && item.TestDate.Year == date.Year && item.TesterId==t.Id);
                    if (invalid_test == null && distance(t.MyAddress, address) < t.MaxDistance) 
                    {
                        valid_tester = t;
                        flag = true;
                        break;

                    }
                    if (flag == false)
                        throw new Exception("There is no Tester that can make the test in:" + date.Day + "/" + date.Month + date.Year + " " + date.Hour + ":" + date.Minute); ;
                }
                    //Check if student exist(lamda)
                 Trainee trainee_exist = all_my_trainees.Find(item => item.Id == trainee_id);
               
                if (trainee_exist == null)
                    throw new Exception("Trainee " + trainee_id + "is not exist!!");
                else
                {
                    //check if Trainee made at least min lessons for test
                    if(trainee_exist.NumberOfLessons<Configuration.MIN_LESSONS_FOR_TEST)
                        throw new Exception("Trainee " + trainee_id + "must do "+(Configuration.MIN_LESSONS_FOR_TEST- trainee_exist.NumberOfLessons)+" for a test!!");
                    else
                    {
                        Test last = all_my_test.Find(x => x.TraineeId == trainee_id && (x.TestDate.Month == DateTime.Now.Month && x.TestDate.Year == DateTime.Now.Year && DateTime.Now.Day - x.TestDate.Day < 7));
                        if (last != null)// trainee made a test less then 7 days ago
                            throw new Exception("Trainee " + last.TraineeId + " must wait " +( 7 - (DateTime.Now.Day - last.TestDate.Day)) + "  days before test!!");
                    }
                }
                
                temp = new Test("0000000000", trainee_id, DateTime.Now, address);
                dal.AddTest(temp);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       public void AddTester(Tester tester)
        {
            ifNonLetters(tester.Id, "Tester ID");
            ifNonLetters(tester.PhoneNumber, "Tester Phone number");
            ValidAddress(tester.MyAddress);

            dal.AddTester(tester);  
            
        }
        void IBL.AddTester(string id, string name, string family_name, DateTime birth_date, gender my_gender, string phone, Address t_adress, int years_of_exprience, int number_of_tests, carType exp, bool[,] work_hours, int max_distance)
        {
           
            try
            {

                Tester temp;
                if (id.Length !=9)
                    // ID must contain 9 dighits only
                    throw new Exception("ID must contain only 9 digits");
                ifNonLetters(id, "ID");
                

                if (  DateTime.Now.Year - birth_date.Year < Configuration.Tester_MIN_AGE)
                    //Tester age can't be younger then min age!!!
                    throw new Exception("Tester cant't be younger then "+Configuration.Tester_MIN_AGE);
                //valid birth date
                if (phone.Length !=10)
                    throw new Exception("Phone Number must contian  only 10 digits" );
                ValidAddress(t_adress);
                if (years_of_exprience > (DateTime.Now.Year-birth_date.Year ) - 18)
                    throw new Exception("A tester can not be experienced more then " +((DateTime.Now.Year-birth_date.Year ) - 18) +" years");
                if (number_of_tests > 30 || number_of_tests < 1)
                    //Number of tets per week can be bigger then 30 or less then 1
                    throw new Exception(" Maximum tests per week must be between 1 to 30");
                if (max_distance < 0)
                    throw new Exception("Maximum distance can't be negative");
                int numberTestrHours = 0;
                foreach (bool b in work_hours) { if (b == true) numberTestrHours++; }
                if (numberTestrHours > number_of_tests)
                    /* Tester can't work more the maximum tests per week*/
                    throw new Exception("Tester can't work more then " + number_of_tests + "per week!!");
                    temp = new Tester(id, name, family_name, birth_date, my_gender, phone, t_adress, years_of_exprience, number_of_tests, exp, max_distance, work_hours);
                dal.AddTester(temp);
                
            }
            
            catch( FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        void IBL.AddTrainee(string id, string name, string familyName, DateTime birthD, gender g, string phoneNum, Address address, carType type, gear my_gear, string school, string teacher_name, int numLessons)
        {
            Trainee temp;
            try
            {
                if (id.Length < 9 || id.Length > 9)
                    // ID must contain 9 dighits only
                    throw new Exception("ID must contain only 9 digits");
                if (phoneNum.Length < 10 || phoneNum.Length > 10)
                    throw new Exception("Phone Number must contian  only 10 digits");
                if(birthD.Year >DateTime.Now.Year-Configuration.Trainee_MIN_AGE)
                throw new Exception("Trainee cant't be younger then " + Configuration.Tester_MIN_AGE);
                if (address.city == null)
                    //address must contin all it fileds
                    throw new Exception("Address must contain city name");
                if (address.streetName == null)
                    throw new Exception("Address must contain street name");
                if (address.houseNumber <= 0)
                    throw new Exception("Address must contain house number");
                if (numLessons <= 0)
                    throw new Exception("Number of lessons must br bigger then 0 ");
    
                temp = new Trainee(id, name, familyName, birthD, g, phoneNum, address, type, my_gear, school, teacher_name, numLessons);
                dal.AddTrainee(temp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        void IBL.DeleteTester(string id)
        {
            try
            {
                dal.DeleteTester(id);
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Data);
            }
        }

        void IBL.DeleteTrainee(string id)
        {
            try
            {
                dal.DeleteTrainee(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Data);
            }
           
        }

        List<Tester> IBL.GetAllTesters()
        {
            return dal.GetAllTesters();
        }

        List<Test> IBL.GetAllTests()
        {
            return dal.GetAllTests();
        }

        List<Trainee> IBL.GetAllTrainees()
        {
            return dal.GetAllTrainees();
        }

        void IBL.UpdateTest(Test test)
        {
            test.IsPass = TestResult(test);
            dal.UpdateTest(test);
        }

        void IBL.UpdateTester(Tester t)
        {
           
        }

        void IBL.UpdateTrainee(Trainee t)
        {
            if (t.Id.Length != 9 )
                // ID must contain 9 dighits only
                throw new Exception("ID must contain only 9 digits");
            ifNonLetters(t.Id, " Trainee ID");
            if (t.PhoneNumber.Length != 10 )
                throw new Exception("Phone Number must contian  only 10 digits");
            ifNonLetters(t.Id, " Trainee phone number");
            if (t.BrithDate.Year > DateTime.Now.Year - Configuration.Trainee_MIN_AGE)
                throw new Exception("Trainee cant't be younger then " + Configuration.Tester_MIN_AGE);
            if (t.MyAddress.city == null)
                //address must contin all it fileds
                throw new Exception("Address must contain city name");
            if (t.MyAddress.streetName == null)
                throw new Exception("Address must contain street name");
            if (t.MyAddress.houseNumber <= 0)
                throw new Exception("Address must contain house number");
            if (t.NumberOfLessons <= 0)
                throw new Exception("Number of lessons must br bigger then 0 ");

          
            dal.AddTrainee(t);
        }
   
    }
    }
