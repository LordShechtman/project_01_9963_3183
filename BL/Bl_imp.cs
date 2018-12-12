using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using BE;
namespace BL
{

    public class Bl_imp : IBL
    {
        
         public  DAL.Dal_imp dal=new DAL.Dal_imp();
          DAL.Factory_dal factory = new DAL.Factory_dal();
          
        //TODO: use this deleagte
        delegate bool myFunc(List<object> lst, string conditon);
        
        public void ifNonLetters(string str,string name)
        {
            //Check for ID and phone number if there are no letters in the string 
            foreach(char ch in str)
            {
                if (ch < '0' || ch > '9')
                    throw new Exception(name + " must contin dighits only!!");
            }
        }

        public int numberOfTests(Trainee T)
        {
          
            IEnumerable<Test > lst= dal.GetAllTests();
            IEnumerable<Test> count = lst.Where(i => i.TraineeId == T.Id).Select(i=>i);
            return count.Count();
            
        }

        public List<Trainee> passedToday()
        {
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

        void IBL.AddTest(string tester_id, string trainee_id, Address address)
        {
           List<Tester> all_my_testers = dal.GetAllTesters();
           List<Trainee> all_my_trainees = dal.GetAllTrainees();
            List<Test> all_my_test = dal.GetAllTests();
            try
            {
                Test temp;
                if (tester_id.Length < 9 || tester_id.Length > 9)
                    throw  new Exception("Tester id must contin  only 9 dights !!");
                if (trainee_id.Length < 9 || trainee_id.Length > 9)
                    throw new Exception("Tester id must contin only 9 dights!!");
                ifNonLetters(tester_id, "Tester ID");
                ifNonLetters(trainee_id, " Trainee ID");
                if (address.city == null)
                    //address must contin all it fileds
                    throw new Exception("Address must contain city name");
                if (address.streetName == null)
                    throw new Exception("Address must contain street name");
                if (address.houseNumber <= 0)
                    throw new Exception("Address must contain house number");
                // Check if tester exist 
                bool exist=false;
              foreach(Tester t in all_my_testers)
                {
                    if (t.Id == tester_id)
                        exist = true;
                }
                if (exist == false)
                    throw new Exception("Tester " + tester_id + "is not exist!!");
                //Check if student exist(lamda)
                 Trainee  count= all_my_trainees.Find(item => item.Id == trainee_id);
               
                if (count == null)
                    throw new Exception("Trainee " + trainee_id + "is not exist!!");
                else
                {
                    //check if Trainee made at least min lessons for test
                    if(count.NumberOfLessons<Configuration.MIN_LESSONS_FOR_TEST)
                        throw new Exception("Trainee " + trainee_id + "must do "+(Configuration.MIN_LESSONS_FOR_TEST-count.NumberOfLessons)+" for a test!!");
                    else
                    {

                    }
                }
                //TODO: FIND IF THEE TRINEE DIDN'T HAVE A TEST LESS THEN 7 DAYS AGO
                temp = new Test(tester_id, trainee_id, DateTime.Now, address);
                dal.AddTest(temp);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void IBL.AddTester(string id, string name, string family_name, DateTime birth_date, gender my_gender, string phone, Address t_adress, int years_of_exprience, int number_of_tests, carType exp, bool[,] work_hours, int max_distance)
        {
           
            try
            {

                Tester temp;
                if (id.Length < 9 || id.Length > 9)
                    // ID must contain 9 dighits only
                    throw new Exception("ID must contain only 9 digits");
                ifNonLetters(id, "ID");
                

                if (  DateTime.Now.Year - birth_date.Year < Configuration.Tester_MIN_AGE)
                    //Tester age can't be younger then min age!!!
                    throw new Exception("Tester cant't be younger then "+Configuration.Tester_MIN_AGE);
                //valid birth date
                if (phone.Length < 10 || phone.Length > 10)
                    throw new Exception("Phone Number must contian  only 10 digits" );
                if (t_adress.city == null)
                    //address must contin all it fileds
                    throw new Exception("Address must contain city name");
                if (t_adress.streetName == null)
                    throw new Exception("Address must contain street name");
                if(t_adress.houseNumber <= 0 )
                    throw new Exception("Address must contain house number");
                if (years_of_exprience > (birth_date.Year - DateTime.Now.Year) - 18)
                    throw new Exception("A tester can not be experienced more then " +( (birth_date.Year - DateTime.Now.Year) - 18) +" years");
                if (number_of_tests > 30 || number_of_tests < 1)
                    //Number of tets per week can be bigger then 30 or less then 1
                    throw new Exception(" Maximum tests per week must be between 1 to 30");
                if (max_distance < 0)
                    throw new Exception("Maximum distance can't be negative");

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

        void IBL.UpdateTest(Test t)
        {
            throw new NotImplementedException();
        }

        void IBL.UpdateTester(Tester t)
        {
            throw new NotImplementedException();
        }

        void IBL.UpdateTrainee(Trainee t)
        {
            throw new NotImplementedException();
        }
   
    }
    }
