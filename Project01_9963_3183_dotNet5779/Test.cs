using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Test :ICloneable
    {
        #region FILEDS
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
            //Update test
            TesterId = tester_id;
            TraineeId = trainee_id;
            TestDate = date_test;
            TestAddress = address;
            IsPass = false;
            TestParameters = parameters;
            TesterNotes = notes;
        }
        public string showParmeters(List< bool> par)
        {
            testsParameters p;
            p = 0;
            string str = "";
            foreach (bool b in par)
            {
                str += p + ":";
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

            return "Test number: " + this.TestNumber + "\n" + "Tester Id: " + this.TesterId + "\n" + "Trainee Id: " + this.TraineeId + "\n" + "Date " + this.TestDate + "\n" + "Test results:" + "\n" + showParmeters(this.TestParameters) + "Notes: \n" + showNotes(this.TesterNotes) + "\n" + "Passed? " + this.IsPass;
        }

        public object Clone()
        {
           return new Test(TesterId,TraineeId,TestDate,TestAddress,TestParameters,TesterNotes);
        }
    }
}
