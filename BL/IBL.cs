using System;
using System.Collections.Generic;
using System.Linq.Expressions; 
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
  public interface IBL
{
    
    //Tester functions;
    void AddTester(string id,string name,string family_name,DateTime birth_date,gender my_gender,string phone,Address t_adress,int years_of_exprience,int number_of_tests,carType exp,bool[,] work_hours,int max_distance);
    void DeleteTester(string id);
    void UpdateTester(Tester t);
    //------------------------------
    void AddTrainee(string id, string name, string familyName, DateTime birthD, gender g, string phoneNum, Address address, carType type, gear my_gear, string school, string teacher_name, int numLessons);
    void DeleteTrainee(string id);
    void UpdateTrainee(Trainee t);
    //--------------------------------
    /// TEST Function
    /// 
    void AddTest(string tester_id, string trainee_id, Address address);
    void UpdateTest(Test t);
    /// Fuctions that counts the number of test for  certin Trainee
    int numberOfTests(Trainee T);
    List<Trainee> passedToday();

    //--------------geting Functions
    List<Tester> GetAllTesters();
    List<Trainee> GetAllTrainees();
    List<Test> GetAllTests();
}
namespace BL
{ 
    
}
