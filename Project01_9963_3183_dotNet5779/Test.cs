using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Test :ICloneable
    {
        /// <summary>
        /// Present a test object
        /// the main key is : Tester ID Trainee Id and Test Date(only one per day)
        /// Test Serial number is given by the DAL layer when IT added to the data base
        /// </summary>
        #region FILEDS
        private readonly string[] TestParmtersNames = { "Keep Distance", "Reverse Parking", "Looking in the Mirors ", "Signals", "Wheel handling", "Priority Rules" };
        public string [] Get_test_parm()
        {
            return TestParmtersNames;
        }
        public string TestNumber { get; set; }

        public string TesterId { get; set; }
        public string TraineeId { get; set; }
        public DateTime TestDate { get; set; }
        //  private DateTime testHour; ---optinal
        public Address TestAddress { get; set; }
       public  List<bool> TestParameters { get; set; }
        public bool IsPass { get; set; }
        public List <string> TesterNotes { get; set; }
        #endregion
        public Test() { }
        public Test(string tester_id, string trainee_id,DateTime date_test,Address address )
        {
            //When we Bulid a test we only ceate the test itslef and we update the test after it
            TesterId = tester_id;
            TraineeId = trainee_id;
            TestDate = date_test;
            TestAddress = address;
            IsPass = false;
            TestParameters = new List<bool>();
            TesterNotes = new List<string>();


        }
        public Test(string tester_id, string trainee_id, DateTime date_test, Address address,List<bool> parameters,List<string>notes)
        {
            //Update test by the Tester
            TestParameters = new List<bool>();
            TesterNotes = new List<string>();
            TesterId = tester_id;
            TraineeId = trainee_id;
            TestDate = date_test;
            TestAddress = address;
            IsPass = false;
            //Deep copy of the lists
            foreach(bool b in parameters)
            TestParameters .Add(b);
            foreach(string note in notes)
            TesterNotes .Add( note);
        }
        public string showParmeters(List< bool> par)
        {
            //Pints the test paramter results!!
            MyEnum.testsParameters p;
            p = 0;
            string str = "";
            foreach (bool b in par)
            {
                str += TestParmtersNames[(int)p]+ ":";
                p++;
                if (b == true)
                    str += "yes";
                else
                    str += "no";

                str += "\n";     
            }
            return str;

          
        }
        public string showNotes(List<string> lst)
        {
            //Prints all the tester notes by parameters
            string str = "";
            if (lst.Count() == 0)
                return null;
            int count = 0;
            foreach(string c in lst)
            {
                count++;
                str += count + "." + c + "\n";

            }
            return str;
        }

        
        public override string ToString()
        {

            return "Test number: " + this.TestNumber + "\n" + "Tester Id: " + this.TesterId + "\n" + "Trainee Id: " + this.TraineeId + "\n" + "Date " + this.TestDate.ToString() + "\n" + "Test results:" + "\n" + showParmeters(this.TestParameters) + "Notes: \n" + showNotes(this.TesterNotes) + "\n" + "Passed? " + this.IsPass;
        }

        public object Clone()
        {
           return new Test(TesterId,TraineeId,TestDate,TestAddress,TestParameters,TesterNotes);
        }
    }
}
