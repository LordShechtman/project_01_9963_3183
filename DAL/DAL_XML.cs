using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;
using System.IO;

namespace DAL
{
    class DAL_XML : Idal
    {
        static string tests_numPath = @"tests_num.xml";
        static XElement tests_num_root;
        static XElement Trainess_Root;
        static string traineesPath = @"TraineesXml.xml";
        static XElement Tests_Root;
        static string testsPath = @"TestsXml.xml";
        static XElement Testers_Root;
        static string testersPath = @"TestersXml.xml";
        public void AddTest(Test t)
        {
           t.TestNumber = get_num_test().ToString("00000000");
            try
            {
                if (!File.Exists(testsPath))
                {
                    Tests_Root = new XElement("Tests");
                    Tests_Root.Save(testsPath);
                }
                Tests_Root = XElement.Load(testsPath);
                XElement id = new XElement("id", t.TestNumber);
                XElement ID_TESTER = new XElement("ID_TESTER", t.TesterId);
                XElement ID_TRAINEE = new XElement("ID_TRAINEE", t.TraineeId);

          
                XElement DATE = new XElement("DATE", t.TestDate);
             
                XElement City = new XElement("City", t.TestAddress.city);
                XElement Street = new XElement("Street", t.TestAddress.streetName);
                XElement Number = new XElement("Number", t.TestAddress.houseNumber);
                XElement Address = new XElement("Address", City, Street, Number);


                XElement Keep_Distance = new XElement("Keep_Distance", false.ToString());
                XElement Reverse_Parking = new XElement("Reverse_Parking", false.ToString());
                XElement Looking_in_the_Mirors = new XElement("Looking_in_the_Mirors", false.ToString());
                XElement Signals = new XElement("Signals", false.ToString());
                XElement Wheel_handling = new XElement("Wheel_handling", false.ToString());
                XElement Priority_Rules = new XElement("Priority_Rules", false.ToString());

                XElement parameters = new XElement("parameters", Keep_Distance, Reverse_Parking, Looking_in_the_Mirors, Signals, Wheel_handling, Priority_Rules);
                XElement Pass = new XElement("Pass", t.IsPass);

                XElement NOTE = new XElement("NOTE", t.TesterNotes);

                Tests_Root.Add(new XElement("test", id, ID_TESTER, ID_TRAINEE, DATE, Address,  Pass, NOTE, parameters));
                Tests_Root.Save(testsPath);
            }
            catch(Exception Ex)
            {

                 //throw new Exception("File upload problem");
            }

        } 

        public void AddTester(Tester t)
        {
            bool flag = true;
            
                flag=Tester_by_id(t.Id);
                if(flag==true)
                throw new Exception("Tester "+t.Id+" is Already exist!!");
               
            
              flag=  Trainee_by_id(t.Id); // need edit
            if(flag==true)
                throw new Exception("There is a trinee with id: " + t.Id + " is Already exist!!");
            
            try
            {
                if (!File.Exists(testersPath))
                {


                    Testers_Root = new XElement("Testers");
                    Testers_Root.Save(testersPath);
                }
                Testers_Root = XElement.Load(testersPath);
                XElement id = new XElement("id", t.Id);
                XElement firstName = new XElement("firstName", t.Name);
                XElement lastName = new XElement("lastName", t.FamilyName);
                XElement name = new XElement("name", firstName, lastName);
                XElement BIRTH_DAY = new XElement("BIRTH_DAY", t.BirthDate);
                XElement City = new XElement("City", t.MyAddress.city);
                XElement Street = new XElement("Street", t.MyAddress.streetName);
                XElement Number = new XElement("Number", t.MyAddress.houseNumber);
                XElement Address = new XElement("Address", City, Street, Number);
                XElement Gender = new XElement("Gender", t.MyGender);
                XElement Type_car = new XElement("Type_car", t.ExpiranceCar);
                XElement seniority = new XElement("seniority", t.YearsOfExperience);
                XElement MaxTestsPerWeek = new XElement("MaxTestsPerWeek", t.MaxTestsPerWeek);
                XElement MaxDistance = new XElement("MaxDistance", t.MaxDistance);
                
                XElement PhoneNumber = new XElement("PhoneNumber", t.PhoneNumber );
                XElement hours = new XElement("hours", t.get_hours_s());
                XElement Password = new XElement("Password", t.Password);
                Testers_Root.Add(new XElement("tester", id, name, BIRTH_DAY, Address, Gender, Type_car, seniority, MaxTestsPerWeek, MaxDistance, MaxDistance, PhoneNumber, hours,Password));
                Testers_Root.Save(testersPath);
            }
            catch
            {
                // throw new Exception("File upload problem");
            }
        }

        public void AddTrainee(Trainee t)
        {

            bool flag = true;

            flag = Tester_by_id(t.Id);
            if (flag == true)
                throw new Exception("Tester " + t.Id + " is Already exist!!");


            flag = Trainee_by_id(t.Id); // need edit
            if (flag == true)
                throw new Exception("There is a trinee with id: " + t.Id + " is Already exist!!");

            try
            {
                if (!File.Exists(traineesPath))
                {
                    Trainess_Root = new XElement("Trainees");
                    Trainess_Root.Save(traineesPath);
                }
                Trainess_Root = XElement.Load(traineesPath);
                XElement id = new XElement("id", t.Id);
                XElement firstName = new XElement("firstName", t.Name);
                XElement lastName = new XElement("lastName", t.FamilyName);
                XElement name = new XElement("name", firstName, lastName);
                XElement BIRTH_DAY = new XElement("BIRTH_DAY", t.BrithDate);
                XElement City = new XElement("City", t.MyAddress.city);
                XElement Street = new XElement("Street", t.MyAddress.streetName);
                XElement Number = new XElement("Number", t.MyAddress.houseNumber);
                XElement Address = new XElement("Address", City, Street, Number);
              
                XElement Gear = new XElement("Gear", t.MyGear);
                XElement Gender = new XElement("Gender", t.MyGender);
                XElement Type_car = new XElement("Type_car", t.MyGear);
                XElement Teacher = new XElement("Teacher", t.TeacherName);
                XElement LESSONS = new XElement("LESSONS", t.NumberOfLessons);
                XElement SCHOOL = new XElement("SCHOOL", t.School);
              //  XElement PhonePrefix = new XElement("phone_number", t.PhonePrefix);
                XElement phone_number = new XElement("phone_number", t.PhoneNumber);
                XElement Password = new XElement("Password", t.Password);
                Trainess_Root.Add(new XElement("student", id, name, BIRTH_DAY, Address,Gear, Gender, Type_car, Teacher, LESSONS, SCHOOL, phone_number , Password));
                Trainess_Root.Save(traineesPath);
            }
            catch
            {
                // throw new Exception("File upload problem");
            }
        }

        public void DeleteTester(string id)
        {

            if (Tester_by_id(id) == false)
                throw new Exception("Tester " + id + " not exist!"); 
            if (!File.Exists(testersPath))
            {


                Testers_Root = new XElement("Testers");
                Testers_Root.Save(testersPath);
                throw new Exception("The list is emapty");
            }
            Testers_Root = XElement.Load(testersPath);

            XElement tester_XElement;
            try
            {
                tester_XElement = (from stu in Testers_Root.Elements()
                                   where stu.Element("id").Value == id
                                   select stu).FirstOrDefault();
                tester_XElement.Remove();
                Testers_Root.Save(testersPath);

            }
            catch
            {

            }
        }

        public void DeleteTrainee(string id)
        {
            Trainee trainee_t = new Trainee();
            if (Trainee_by_id(id) == false)
                throw new Exception("Trainee " + id + " not exist");


            if (!File.Exists(traineesPath))
            {


                Trainess_Root = new XElement("Trainees");
                Trainess_Root.Save(traineesPath);
                throw new Exception("The list is emapty");
            }
            Trainess_Root = XElement.Load(traineesPath);

            XElement trainee_XElement;
            try
            {
                trainee_XElement = (from stu in Trainess_Root.Elements()
                                    where stu.Element("id").Value == id
                                    select stu).FirstOrDefault();
                trainee_XElement.Remove();
                Trainess_Root.Save(traineesPath);

            }
            catch
            {

            }
        }

        public List<Tester> GetAllTesters()
        {
            try
            {
                Testers_Root = XElement.Load(testersPath);
            }
            catch (Exception)
            {
                
            }

            List<Tester> testers;
            try
            {
                testers = (from stu in Testers_Root.Elements()
                           select new Tester()
                           {
                               Id = stu.Element("id").Value,
                               Name = stu.Element("name").Element("firstName").Value,
                               FamilyName = stu.Element("name").Element("lastName").Value,
                               BirthDate = DateTime.Parse(stu.Element("BIRTH_DAY").Value),
                               MyAddress = Configuration.MAKE_adress(stu.Element("Address").Element("City").Value, stu.Element("Address").Element("Street").Value, int.Parse(stu.Element("Address").Element("Number").Value)),

                               MyGender = Configuration.make_gender(stu.Element("Gender").Value),
                               ExpiranceCar = Configuration.MAKE_TYPE_CAR(stu.Element("Type_car").Value),
                               YearsOfExperience = int.Parse(stu.Element("seniority").Value),
                               MaxTestsPerWeek = int.Parse(stu.Element("MaxTestsPerWeek").Value),
                               MaxDistance = float.Parse(stu.Element("MaxDistance").Value),
                               PhoneNumber = stu.Element("phone_number").Value,
                               hours_string = stu.Element("hours").Value,
                               Password = stu.Element("Password").Value,

                           }).ToList();
            }
            catch (Exception Ex)
            {
                string st = Ex.ToString();
                testers = null;
            }
            foreach (var item in testers)
            {
                item.set_hours_s(item.hours_string);
                item.hours_string = "";
            }
            return testers;
        }

        public List<Test> GetAllTests()
        {
            try
            {
                Tests_Root = XElement.Load(testsPath);
            }
            catch (Exception)
            {


            }

            List<Test> tests = new List<Test>();
            try
            {
                tests = (from stu in Tests_Root.Elements()
                         select new Test()
                         {

                             TestNumber = stu.Element("id").Value,
                             //GET Trinne id
                             TesterId = (stu.Element("ID_TESTER").Value),
                             TraineeId = (stu.Element("ID_TRAINEE").Value),

                             TestDate = DateTime.Parse(stu.Element("DATE").Value),
                             
                             TestAddress = Configuration.MAKE_adress(stu.Element("Address").Element("City").Value, stu.Element("Address").Element("Street").Value, int.Parse(stu.Element("Address").Element("Number").Value)),
                             
                             TestParameters = Configuration.SetBoolLIST(stu.Element("parameters").Element("Keep_Distance").Value, stu.Element("parameters").Element("Reverse_Parking").Value, stu.Element("parameters").Element("Looking_in_the_Mirors").Value, stu.Element("parameters").Element("Signals").Value, stu.Element("parameters").Element("Wheel_handling").Value, stu.Element("parameters").Element("Priority_Rules").Value),
                             
                             TesterNotes = Configuration.MakeNoteList(stu.Element("NOTE").Value),

                             IsPass =Boolean.Parse(stu.Element("Pass").Value),






                         }).ToList();
            }
            catch
            {

            }
            return tests;

        }
        public List<Trainee> GetAllTrainees()
        {
            try
            {
                Trainess_Root = XElement.Load(traineesPath);
            }
            catch (Exception)
            {


            }

            List<Trainee> trainees;
            try
            {
                trainees = (from stu in Trainess_Root.Elements()
                            select new Trainee()
                            {
                                Id = stu.Element("id").Value,
                                Name = stu.Element("name").Element("firstName").Value,
                                FamilyName = stu.Element("name").Element("lastName").Value,
                                BrithDate = DateTime.Parse(stu.Element("BIRTH_DAY").Value),
                                MyGender = Configuration.make_gender(stu.Element("Gender").Value),
                                PhoneNumber = stu.Element("phone_number").Value,

                                MyAddress = Configuration.MAKE_adress(stu.Element("Address").Element("City").Value, stu.Element("Address").Element("Street").Value, int.Parse(stu.Element("Address").Element("Number").Value)),

                                Car = Configuration.MAKE_TYPE_CAR(stu.Element("Type_car").Value),

                                MyGear = Configuration.MAKE_gear(stu.Element("Gear").Value),
                                School = (stu.Element("SCHOOL").Value),
                                TeacherName = (stu.Element("Teacher").Value),
                                NumberOfLessons = int.Parse(stu.Element("LESSONS").Value),
                                Password = (stu.Element("Password").Value),

                            }).ToList();
            }
            catch
            {
                trainees = null;
            }
            return trainees;
        }

        public void UpdateTest(Test t)
        {
            try
            {
                Tests_Root = XElement.Load(testsPath);
            }
            catch (Exception)
            {


            }
            Test test_t = new Test();
            try
            {
                XElement testElement = (from stu in Tests_Root.Elements()
                                        where(stu.Element("id").Value) == t.TestNumber
                                        select stu).FirstOrDefault();

                string temp = "";
                foreach (string item in t.TesterNotes)
                {
                    temp += "." + item;
                }
                testElement.Element("NOTE").Value = temp;
                testElement.Element("parameters").Element("Keep_Distance").Value = t.TestParameters[0].ToString();
                testElement.Element("parameters").Element("Reverse_Parking").Value = t.TestParameters[1].ToString();
                testElement.Element("parameters").Element("Looking_in_the_Mirors").Value = t.TestParameters[2].ToString();
                testElement.Element("parameters").Element("Signals").Value = t.TestParameters[3].ToString();
                testElement.Element("parameters").Element("Wheel_handling").Value = t.TestParameters[4].ToString();
                testElement.Element("parameters").Element("Priority_Rules").Value = t.TestParameters[5].ToString();
                testElement.Element("Pass").Value = t.IsPass.ToString();





                Tests_Root.Save(testsPath);


            }
            catch
            {
                throw new Exception("Not exist this test");
            }
        }
        public int get_num_test()
        {
            try
            {

                if (!File.Exists(tests_numPath))
                {


                    tests_num_root = new XElement("Configuration");
                    tests_num_root.Add(new XElement("num_test", 1));
                    tests_num_root.Save(tests_numPath);
                    return 1;

                }
                else
                {
                    try
                    {
                        tests_num_root = XElement.Load(tests_numPath);
                    }
                    catch (Exception)
                    {


                    }

                    XElement num_t = (from stu in tests_num_root.Elements()

                                      select stu).FirstOrDefault();
                    int num = int.Parse(num_t.Value);
                    num++;
                    num_t.Value = num + "";
                    //  num_t.Value = num + "";


                    tests_num_root.Save(tests_numPath);

                    return num;
                }

            }
            catch (Exception)
            {


            }
            return 0;
        }
        public void UpdateTester(Tester t)
        {
            try
            {
                Testers_Root = XElement.Load(testersPath);
            }
            catch (Exception)
            {


            }
            Tester tester_t = new Tester();
            try
            {
                XElement testerElement = (from stu in Testers_Root.Elements()
                                          where stu.Element("id").Value == t.Id
                                          select stu).FirstOrDefault();
                testerElement.Element("Gender").Value = t.MyGender.ToString();
                testerElement.Element("BIRTH_DAY").Value = t.BirthDate.ToString();
                testerElement.Element("Type_car").Value = t.ExpiranceCar.ToString();
                testerElement.Element("seniority").Value = t.YearsOfExperience.ToString();
                testerElement.Element("MaxTestsPerWeek").Value = t.MaxTestsPerWeek.ToString();
                testerElement.Element("MaxDistance").Value = t.MaxDistance.ToString();
                testerElement.Element("PhoneNumber").Value = t.PhoneNumber;
                testerElement.Element("hours").Value = t.get_hours_s();
                //      testerElement.Element("Email").Value = tester.email;
                testerElement.Element("name").Element("firstName").Value = t.Name;
                testerElement.Element("name").Element("lastName").Value = t.FamilyName;
                testerElement.Element("Address").Element("City").Value = t.MyAddress.city;
                testerElement.Element("Address").Element("Street").Value = t.MyAddress.streetName;
                testerElement.Element("Address").Element("Number").Value = t.MyAddress.houseNumber.ToString();
                testerElement.Element("Password").Value = t.Password;
                Testers_Root.Save(testersPath);


            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }

        public void UpdateTrainee(Trainee t)
        {
            try
            {
                Trainess_Root = XElement.Load(traineesPath);
            }
            catch (Exception)
            {


            }
            Trainee trainee_t = new Trainee();
            try
            {
                XElement traineeElement = (from stu in Trainess_Root.Elements()
                                           where (stu.Element("id").Value) == t.Id
                                           select stu).FirstOrDefault();
                traineeElement.Element("BIRTH_DAY").Value = t.BrithDate.ToString();
                traineeElement.Element("Gender").Value = t.MyGender.ToString();
                traineeElement.Element("Type_car").Value = t.Car.ToString();
                traineeElement.Element("Teacher").Value = t.TeacherName.ToString();
                traineeElement.Element("LESSONS").Value = t.NumberOfLessons.ToString();
                traineeElement.Element("SCHOOL").Value = t.School;
                traineeElement.Element("phone_number").Value = t.PhoneNumber;
                traineeElement.Element("Gear").Value = t.MyGear.ToString();
                traineeElement.Element("Password").Value = t.Password;
                traineeElement.Element("name").Element("firstName").Value = t.Name;
                traineeElement.Element("name").Element("lastName").Value = t.FamilyName;
                traineeElement.Element("Address").Element("City").Value = t.MyAddress.city;
                traineeElement.Element("Address").Element("Street").Value = t.MyAddress.streetName;
                traineeElement.Element("Address").Element("Number").Value = t.MyAddress.houseNumber.ToString();
                Trainess_Root.Save(traineesPath);


            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }
        public bool Trainee_by_id(string id)
        {
            try
            {
                Trainess_Root = XElement.Load(traineesPath);
            }
            catch (Exception)
            {
            }
            Trainee trainee  =null;
            
                trainee = (from stu in Trainess_Root.Elements()
                           where (stu.Element("id").Value) == id
                           select new Trainee()
                           {
                               Id = stu.Element("id").Value
                             
                              

                           }).FirstOrDefault();
            return (trainee != null);
                
           
            
            
            
        }
        
        public bool Tester_by_id(string id)
        {

            try
            {
                Testers_Root = XElement.Load(testersPath);
            }
            catch (Exception)
            {


            }
            Tester tester = null;
           
                tester = (from stu in Testers_Root.Elements()
                          where stu.Element("id").Value == id
                          select new Tester()
                          {
                              Id = stu.Element("id").Value,
                              


                          }).FirstOrDefault();
                tester.set_hours_s(tester.hours_string);
                tester.hours_string = "";
                return  (tester != null) ;
                
            }
            

        



    }

}
