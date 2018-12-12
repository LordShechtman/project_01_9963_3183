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
     public  Dal_imp()
        {
        }  
           public void AddTester(Tester t)
        {
            //Add new tester to the testers list (After the object approval  by the BL)
            foreach (Tester tmp in DS.DataSource.testers)
            {
                if (tmp.Id == t.Id)
                    throw new Exception("Tester " + t.Id + " is already exists!");
            }
            DS.DataSource.testers.Add(t);

        }
       public  void DeleteTester(string id)
        {
            bool flag = false;
            foreach (Tester tmp in DS.DataSource.testers)
            {
                if (tmp.Id == id)
                {
                    DS.DataSource.testers.Remove(tmp);
                    flag = true;
                    break;
                }
            }
            if (flag == false)
                throw new Exception("Tester "+id+ " is not Exists!");
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
            //------------get 8 dighits test number---------------------------
            int x = Configuration.testNum, dightcount = 0;
            string code = "";

            while (x > 0)
            {
                dightcount++;
                x /= 10;
            }
            for (int i = 0; i <= 8 - dightcount; i++)
                code += "0";
            code += Configuration.testNum;
            t.TestNumber = code;
            //------------------------------------------------------------------------------------------------
            DS.DataSource.tests.Add(t);
        }
        public void UpdateTest(Test t)
        {
            bool flag = false;
            foreach (Test tmp in DS.DataSource.tests)
            {
                if (tmp.TestNumber == t.TestNumber)
                {
                    flag = true;
                    DS.DataSource.tests.Remove(tmp);
                    DS.DataSource.tests.Add(t);
                }
            }
            if (flag == false)
                throw new Exception();
        }

        //--------------geting Functions
      public  List<Tester> GetAllTesters() { return DS.DataSource.testers; }
      public  List<Trainee> GetAllTrainees() { return DS.DataSource.trainees; }
      public  List<Test> GetAllTests() { return DS.DataSource.tests; }
    

        }
    }

