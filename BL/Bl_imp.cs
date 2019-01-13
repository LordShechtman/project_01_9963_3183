using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using BE;
namespace BL
{
    /// <summary>
    /// The BL layer
    /// In this layer we chek the input before it go to the DAL Layer
    /// In addtion we pull information from the Dal and gives to the PL 
    /// the fillter data the the user wants to know.
    /// 
    /// </summary>
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
            return 50;
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


        public bool IsSameCarType(string tester_id, string trainee_id)
        {
            //Returns if the the tester and the trainee use the same car Tape (for test)
            Tester tester = dal.GetAllTesters().Find(x => x.Id == tester_id);
            Trainee trainee = dal.GetAllTrainees().Find(y => y.Id == trainee_id);
            if (tester == null)
                throw new Exception("Tester" + tester_id + " not exist");
            if (tester == null)
                throw new Exception("Trainee" + trainee_id + " not exist");
            return tester.ExpiranceCar == trainee.Car;

        }
        public void IfNonLetters(string str, string name)
        {
            //Check for ID and phone number if there are no letters in the string 
            foreach (char ch in str)
            {
                if (ch < '0' || ch > '9')
                    throw new Exception(name + " must contin dighits only!!");
            }
        }
        public bool TestResult(Test test)
        {
            //Detrminate the reuslt of the test
            int count = test.TestParameters.Count(item => item == true);
            return count >= Configuration.Number_Parameters_To_Pass
                && test.TestParameters[(int)MyEnum.testsParameters.wheelhandling] == true
                && test.TestParameters[(int)MyEnum.testsParameters.keepDistance] == true
                && test.TestParameters[(int)MyEnum.testsParameters.priorityrules] == true;
        }
        private bool InTheSameWeek(DateTime date1, DateTime date2)
        {
            var d1 = date1.AddDays(-1 * (int)date1.DayOfWeek);
            var d2 = date2.AddDays(-1 * (int)date2.DayOfWeek);
            return d1 == d2;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Returns the Statistic of passing a test
        /// </summary>
        /// <returns></returns>
        public double PassStatistic()
        {
            int Pass_number = dal.GetAllTests().Count(y => y.IsPass == true);

            return (double)Pass_number / dal.GetAllTests().Count();
        }
        public int numberOfTests(Trainee T)
        {
            /*Using lambda exprssion to find how many tests certin student made*/
            IEnumerable<Test> lst = dal.GetAllTests();
            IEnumerable<Test> count = lst.Where(i => i.TraineeId == T.Id).Select(i => i);
            return count.Count();

        }
        public IEnumerable<IGrouping<int, Tester>> TotalTestsByTester()
        {
            //Returns all Testersgroup by the test they have made
            IEnumerable<IGrouping<int, Tester>> num = from x in dal.GetAllTesters()
                                                      let toatalTests = allTestBy(y => y.TesterId == x.Id).Count()
                                                      group x by toatalTests;


            return num;
        }

        public List<Trainee> passedToday()
        {
            //retuns a list of students who passed Today(   Date Time.now)
            List<Trainee> passed = new List<Trainee>();
            List<Trainee> lst = dal.GetAllTrainees();
            foreach (Test T in dal.GetAllTests())
            {
                if (T.IsPass == true && (T.TestDate.Day == DateTime.Now.Day && T.TestDate.Month == DateTime.Now.Month && T.TestDate.Year == DateTime.Now.Year))
                {
                    foreach (Trainee trainee in lst)
                    {
                        if (trainee.Id == T.TraineeId)
                        {
                            passed.Add(trainee);
                            break;
                        }
                    }
                }
            }
            return passed;
        }
        public IEnumerable<Tester> TestersByOrder()
        {
            IEnumerable<Tester> orderList = from item in dal.GetAllTesters()
                                            orderby item.FamilyName, item.Name
                                            select item;
            return orderList;
        }
        public bool isTraineePassed(string id)
        {

            Test passed = dal.GetAllTests().Find(item => item.TraineeId == id && item.IsPass == true);
            return passed != null;
        }
        public List<Tester> avilableTesters(int hour, MyEnum.days day)
        {
            IEnumerable<Tester> able = from item in dal.GetAllTesters()
                                       where item.WorkHours[(int)day, hour - 9] == true
                                       select item;
            return able.ToList();
        }
        public List<Tester> liveFrom(int x, Address address)
        {
            IEnumerable<Tester> live_from = from item in dal.GetAllTesters()
                                            where distance(item.MyAddress, address) == x
                                            orderby item.Name, item.FamilyName, item.Id
                                            select item;

            ;

            return live_from.ToList();
        }
        public List<Test> allTestBy(Predicate<Test> codition)
        {

            return dal.GetAllTests().FindAll(codition);



        }

        public List<Test> Testbydate(DateTime date)
        {

            IEnumerable<Test> indate = from item in dal.GetAllTests()
                                       where item.TestDate.Month == date.Month
                                       select item;
            indate.GroupBy(item => item.TestDate.Day);
            return indate.ToList();
        }


        /// <summary>
        /// flag =bool exprssion (orderd or not)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, Trainee>> TraineeBySchool(bool flag)
        {
            IEnumerable<Trainee> All_Trainees = dal.GetAllTrainees();
            if (flag == false)
            {

                var DrvingSchool = from item in All_Trainees
                                   group item by item.School;
                return DrvingSchool;
            }
            var DrvingSchoolOrderd = from item in All_Trainees
                                     orderby item.FamilyName, item.Name
                                     group item by item.School;
            return DrvingSchoolOrderd;
        }
        public IEnumerable<IGrouping<MyEnum.carType, Tester>> TestersByCarExpriance(bool flag)
        {
            IEnumerable<Tester> All_Testers = dal.GetAllTesters();
            if (flag == false)
            {

                var CarExpirance = from item in All_Testers
                                   group item by item.ExpiranceCar;
                return CarExpirance;
            }
            var CarExpiranceByOrder = from item in All_Testers
                                      orderby item.FamilyName, item.Name
                                      group item by item.ExpiranceCar;
            return CarExpiranceByOrder;

        }
        public IEnumerable<IGrouping<int, Trainee>> AllTraineesByNumberOfTests()
        {
            IEnumerable<Trainee> trainees = dal.GetAllTrainees();
            IEnumerable<Test> tests = dal.GetAllTests();
            var TraineesBytest = from item in trainees
                                 group item by numberOfTests(item);
            return TraineesBytest;


        }
        public IEnumerable<IGrouping<string, Trainee>> TraineeByTeacher(bool flag)
        {
            IEnumerable<Trainee> All_Trainees = dal.GetAllTrainees();
            if (flag == false)
            {

                var DrvingSchool = from item in All_Trainees
                                   group item by item.TeacherName;
                return DrvingSchool;
            }
            var DrvingSchoolOrderd = from item in All_Trainees
                                     orderby item.FamilyName, item.Name
                                     group item by item.TeacherName;
            return DrvingSchoolOrderd;
        }
        //public void ValidId(string my_id)
        #endregion
        #region Adding Metods
        void IBL.AddTest(string trainee_id, Address address, DateTime date)
        {
            List<Tester> all_my_testers = dal.GetAllTesters();
            List<Trainee> all_my_trainees = dal.GetAllTrainees();
            List<Test> all_my_test = dal.GetAllTests();

            Test temp;

            //// Find tester that can make the test in that data


            IfNonLetters(trainee_id, " Trainee ID");
            ValidAddress(address);
            if (date.Hour > 16 || date.Hour < 9)
                throw new Exception("Test can only be between 9:00-16:00!!");
            if ((int)date.DayOfWeek > 4)
                throw new Exception("Tests can only be happened between Sunday to Thursday");
            Tester validTester = null;
            //All TESTERS WHO WORK IN DAY X OF THE WEEK AND HOUR Y IN THAT DAY
            var valid_testers = all_my_testers.FindAll(item => item.WorkHours[(int)date.DayOfWeek, (int)date.Hour - 9] == true);
            //All Testers who Work in that day in the same Hour(so they not aviable to the test)
            List<Test> thisDay = all_my_test.FindAll(x => x.TestDate == date && x.TestDate.Hour==date.Hour);
            //All Tests in that week
            List<Test> thisWeek = all_my_test.FindAll(x => InTheSameWeek(date, x.TestDate));
            //Find aviable tester
            foreach (var tester in valid_testers)
            {
                //Find if the tester is not have test in that day and that hour
                var freeTester = from x in thisDay
                                 where x.TesterId == tester.Id
                                 select x;
                //Count All the test in that Week
                var testThisWeek = from y in thisWeek
                                   where y.TesterId == tester.Id
                                   select y;

                if (freeTester.Any() == false
                    && testThisWeek.Count() <= tester.MaxTestsPerWeek
                    && tester.MaxDistance >= distance(tester.MyAddress, address)
                    && IsSameCarType(tester.Id, trainee_id) == true)
                {
                    validTester = tester;
                    break;
                }
            }

            if (validTester == null)
                throw new Exception("There is no Tester that can make the test in:"
                  + date.Day + "/" + date.Month + "/" + date.Year + " " + date.Hour + ":00");




            //Check if student exist(lamda)
            Trainee trainee_exist = all_my_trainees.Find(item => item.Id == trainee_id);

            if (trainee_exist == null)
                throw new Exception("Trainee " + trainee_id + "is not exist!!");
            else
            {
                //check if Trainee made at least min lessons for test
                if (trainee_exist.NumberOfLessons < Configuration.MIN_LESSONS_FOR_TEST)
                    throw new Exception("Trainee " + trainee_id + "must do " +
                    (Configuration.MIN_LESSONS_FOR_TEST - trainee_exist.NumberOfLessons) + "  lessons for a test!!");
                else
                {
                    Test last = all_my_test.Find(x => x.TraineeId == trainee_id && ((date - x.TestDate).Days < 7));
                    if (last != null)// trainee made a test less then 7 days ago
                        throw new Exception("Trainee " + last.TraineeId + " must wait " + (7 - (date - last.TestDate).Days) + "  days before test!!");
                }
            }

            temp = new Test(validTester.Id, trainee_id, date, address);
            dal.AddTest(temp);


        }
        public void AddTester(Tester tester)
        {
            IfNonLetters(tester.Id, "Tester ID");
            IfNonLetters(tester.PhoneNumber, "Tester Phone number");
            ValidAddress(tester.MyAddress);

            dal.AddTester(tester);

        }
        void IBL.AddTester(string id, string name, string family_name, DateTime birth_date, MyEnum.gender my_gender, string phone, Address t_adress, int years_of_exprience, int number_of_tests, MyEnum.carType exp, bool[,] work_hours, int max_distance)
        {



            Tester temp;
            if (id.Length != 9)
                // ID must contain 9 dighits only
                throw new Exception("ID must contain only 9 digits");
            IfNonLetters(id, "ID");


            if (DateTime.Now.Year - birth_date.Year < Configuration.Tester_MIN_AGE)
                //Tester age can't be younger then min age!!!
                throw new Exception("Tester cant't be younger then " + Configuration.Tester_MIN_AGE);
            //valid birth date

            ValidAddress(t_adress);
            if (years_of_exprience > (DateTime.Now.Year - birth_date.Year) - 18)
                throw new Exception("A tester can not be experienced more then " + ((DateTime.Now.Year - birth_date.Year) - 18) + " years");
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
        public void AddTrainee(Trainee trainee)
        {
            if (trainee.Id.Length != 9)
                // ID must contain 9 dighits only
                throw new Exception("ID must contain only 9 digits");

            if (trainee.BrithDate.Year > DateTime.Now.Year - Configuration.Trainee_MIN_AGE)
                throw new Exception("Trainee cant't be younger then " + Configuration.Trainee_MIN_AGE);
            ValidAddress(trainee.MyAddress);
            if (trainee.NumberOfLessons <= 0)
                throw new Exception("Number of lessons must be bigger then 0 ");
            dal.AddTrainee(trainee);
        }
        void IBL.AddTrainee(string id, string name, string familyName, DateTime birthD, MyEnum.gender g, string phoneNum, Address address, MyEnum.carType type, MyEnum.gear my_gear, string school, string teacher_name, int numLessons)
        {
            Trainee temp;

            if (id.Length < 9 || id.Length > 9)
                // ID must contain 9 dighits only
                throw new Exception("ID must contain only 9 digits");

            if (birthD.Year > DateTime.Now.Year - Configuration.Trainee_MIN_AGE)
                throw new Exception("Trainee cant't be younger then " + Configuration.Trainee_MIN_AGE);
            ValidAddress(address);
            if (numLessons <= 0)
                throw new Exception("Number of lessons must br bigger then 0 ");

            temp = new Trainee(id, name, familyName, birthD, g, phoneNum, address, type, my_gear, school, teacher_name, numLessons);

            dal.AddTrainee(temp);



        }
        #endregion
        #region Delete Methods
        void IBL.DeleteTester(string id)
        {
            try
            {
                dal.DeleteTester(id);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Data);
            }
        }

        void IBL.DeleteTrainee(string id)
        {

            dal.DeleteTrainee(id);



        }
        #endregion
        #region  Get List Methods
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
        #endregion
        #region Update
        void IBL.UpdateTest(Test test)
        {

            test.IsPass = TestResult(test);
            dal.UpdateTest(test);
        }

        void IBL.UpdateTester(Tester tester)
        {
            if (tester.Id.Length != 9)
                // ID must contain 9 dighits only
                throw new Exception("ID must contain only 9 digits");
            IfNonLetters(tester.Id, "ID");


            if (DateTime.Now.Year - tester.BirthDate.Year < Configuration.Tester_MIN_AGE)
                //Tester age can't be younger then min age!!!
                throw new Exception("Tester cant't be younger then " + Configuration.Tester_MIN_AGE);
            //valid birth date
            ValidAddress(tester.MyAddress);
            if (tester.YearsOfExperience > (DateTime.Now.Year - tester.BirthDate.Year) - 18)
                throw new Exception("A tester can not be experienced more then " + ((DateTime.Now.Year - tester.BirthDate.Year) - 18) + " years");
            if (tester.MaxTestsPerWeek > 30 || tester.MaxTestsPerWeek < 1)
                //Number of tets per week can be bigger then 30 or less then 1
                throw new Exception(" Maximum tests per week must be between 1 to 30");
            if (tester.MaxDistance < 0)
                throw new Exception("Maximum distance can't be negative");
            int numberTestrHours = 0;
            foreach (bool b in tester.WorkHours) { if (b == true) numberTestrHours++; }
            if (numberTestrHours > tester.MaxTestsPerWeek)
                /* Tester can't work more the maximum tests per week*/
                throw new Exception("Tester can't work more then " + tester.MaxTestsPerWeek + "per week!!");

            dal.UpdateTester(tester);

        }

        void IBL.UpdateTrainee(Trainee t)
        {
            if (t.Id.Length != 9)
                // ID must contain 9 dighits only
                throw new Exception("ID must contain only 9 digits");
            IfNonLetters(t.Id, " Trainee ID");
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


            dal.UpdateTrainee(t);
        }


        #endregion
        #region setPassword
        public void SetTesterpassword(string id, string password)
        {
            Tester x = dal.GetAllTesters().Find(y => y.Id == id);
            if (x == null)
                throw new Exception("Tester" + id + " not found");
            x.Password = password;
            dal.UpdateTester(x);
        }
        public void SetTraineePassword(string id, string password)
        {
            Trainee x = dal.GetAllTrainees().Find(y => y.Id == id);
            if (x == null)
                throw new Exception("Tester" + id + " not found");
            x.Password = password;
            dal.UpdateTrainee(x);
        }

        #endregion
    }
    }
