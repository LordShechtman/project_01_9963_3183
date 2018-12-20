using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
  public interface IBL
{
    #region Tester
    //Tester functions;
    void AddTester(Tester tester);
    void AddTester(string id,string name,string family_name,DateTime birth_date,gender my_gender,string phone,Address t_adress,int years_of_exprience,int number_of_tests,carType exp,bool[,] work_hours,int max_distance);
    void DeleteTester(string id);
    void UpdateTester(Tester t);
    #endregion
    #region Trainee
    void AddTrainee(string id, string name, string familyName, DateTime birthD, gender g, string phoneNum, Address address, carType type, gear my_gear, string school, string teacher_name, int numLessons);
    void AddTrainee(Trainee trainee);
    void DeleteTrainee(string id);
    void UpdateTrainee(Trainee t);
    #endregion
    #region Test
    void AddTest( string trainee_id, Address address, DateTime date);
    void UpdateTest(Test t);
    #endregion
    #region Methods( Using by link ,lambda and so)
    
     double PassStatistic();
    IEnumerable<IGrouping<int, Tester>> TotalTestsByTester();
    int numberOfTests(Trainee T);
    List<Trainee> passedToday();
    IEnumerable<IGrouping<string, Trainee>>TraineeBySchool(bool flag);
    IEnumerable<IGrouping<carType, Tester>> TestersByCarExpriance(bool flag);
    IEnumerable<IGrouping<int, Trainee>> AllTraineesByNumberOfTests();
    IEnumerable<IGrouping<string, Trainee>> TraineeByTeacher(bool flag);
    IEnumerable<Tester> TestersByOrder();
    List<Test> allTestBy(Predicate<Test> codition);

    List<Test> Testbydate(DateTime date);
    List<Tester> liveFrom(int x, Address address);
    
    #endregion
    //--------------geting Functions
    List<Tester> GetAllTesters();
    List<Trainee> GetAllTrainees();
    List<Test> GetAllTests();
   
}
namespace BL
{ 
    
}
