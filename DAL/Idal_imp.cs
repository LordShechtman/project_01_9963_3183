using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{

    public class Dal_imp : Idal

    {
        #region Singleton
        public static readonly Dal_imp instance = new Dal_imp();
        public static Dal_imp Instance
        {
            get { return instance; }
        }

        private Dal_imp() {
            DS.DataSource.testers = new List<Tester>();
            DS.DataSource.trainees = new List<Trainee>();
            DS.DataSource.tests = new List<Test>();
        }
        static Dal_imp() { }

        #endregion
        
        public void AddTester(Tester t)
        {
            //Add new tester to the testers list (After the object approval  by the BL)
            if (DS.DataSource.testers == null)
            {
                
                DataSource.testers.Add(t);
                return;
            }
            foreach (Tester tmp in DS.DataSource.testers)
            {
               
                if (tmp.Id == t.Id)
                    throw new Exception("Tester " + t.Id + " is already exists!");
            }
            DS.DataSource.testers.Add(t);
            return;
        }
       public  void DeleteTester(string id)
        {
            Tester removed=null;
            bool flag = false;
            foreach (Tester tmp in DS.DataSource.testers)
            {
                if (tmp.Id == id)
                {
                    removed = tmp;
                    DS.DataSource.testers.Remove(tmp);
                    flag = true;
                    break;
                }
            }
            if (flag == false)
                throw new Exception("Tester "+id+ " is not Exists!");
            Console.WriteLine("Tester:{0} {1} {2} was  successfully removed ", removed.Id, removed.Name, removed.FamilyName);
        }
       public  void UpdateTester(Tester t)
        {
            bool flag = false;
            foreach (Tester tmp in DS.DataSource.testers)
            {
                if (tmp.Id == t.Id)
                {
                    DS.DataSource.testers.Remove(tmp);
                    DS.DataSource.testers.Add(t);
                    flag = true;
                    break;
                }
            }
            if (flag == false)
                throw new Exception("Tester " + t.Id + " is not exists!");
        }
        //----------------------------------
        // Trinnee functions

       public void AddTrainee(Trainee t)
        {
            if (DS.DataSource.trainees == null)
            {
                DS.DataSource.trainees = new List<Trainee>();
                DataSource.trainees.Add(t);
                return;

            }
            foreach (Trainee tmp in DS.DataSource.trainees)
            {
                if (tmp.Id == t.Id)
                    throw new Exception("Trainee"+t.Id+ "is already exist");
            }
            DS.DataSource.trainees.Add(t);
        }
        public void DeleteTrainee(string id)
        {
            bool flag = false;
            foreach (Trainee tmp in DS.DataSource.trainees)
            {
                if (tmp.Id == id)
                {
                    DS.DataSource.trainees.Remove(tmp);
                    flag = true;
                    break;
                }
            }
            if (flag == false)
                throw new Exception("Trainee" + id + "is not exist");
        }
       public void UpdateTrainee(Trainee t)
        {
            bool flag = false;
            foreach (Trainee tmp in DS.DataSource.trainees)
            {
                if (tmp.Id == t.Id)
                {
                    DS.DataSource.trainees.Remove(tmp);
                    DS.DataSource.trainees.Add(t);

                    flag = true;
                    break;
                }
            }
            if (flag == false)
                throw new Exception("Trainee" + t.Id + "is not exist");

        }
        //--------------------------------
        /// TEST Function
        /// 
       public void AddTest(Test t)
        {
            if(DS.DataSource.tests==null)
            {
                DS.DataSource.tests = new List<Test>();
            }
            // Genrate tests number
            t.TestNumber = Configuration.testNum. ToString("00000000");
            Configuration.testNum++;
            
            
            //------------------------------------------------------------------------------------------------
            DS.DataSource.tests.Add(t);
        }
        public void UpdateTest(Test t)
        {
           
            Predicate<Test> exist = (Test x) => { return x.TestDate == t.TestDate 
                && x.TesterId == t.TesterId 
                && x.TraineeId == t.TraineeId; };
           Test tmp= DS.DataSource.tests.Find(exist);
            if (tmp == null)
                throw new Exception("the test doesn't exist!!");
            t.TestNumber = tmp.TestNumber;
            DS.DataSource.tests.RemoveAll(exist);
            DataSource.tests.Add(t);
                
        }

        //--------------geting Functions
      public  List<Tester> GetAllTesters() { return DS.DataSource.testers; }
      public  List<Trainee> GetAllTrainees() { return DS.DataSource.trainees; }
      public  List<Test> GetAllTests() { return DS.DataSource.tests; }
    

        }
    }

