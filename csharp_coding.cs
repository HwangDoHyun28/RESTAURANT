using System;
using System.Collections.Generic;

public class Test
{
  public static void Main()
  {
    MyOwner JB = new MyOwner("JB");
    MyCook AB = new MyCook("Abyot");
    MyServer HN = new MyServer("Hyun");
    MyMenu JS = new MyMenu("정식", 5000);
    MyMenu TS = new MyMenu("특식", 7000);
    MyMenu CL = new MyMenu("콜라", 2000);
    MyMenu CD = new MyMenu("사이다", 2000);
    JS.MakeMPrice(HN);
    TS.MakeMPrice(HN);
    CL.MakeMPrice(HN);
    CD.MakeMPrice(HN);

    MyCustomer YJ = new MyCustomer("Yejin");
    Dictionary<string, int> Ord1 = new Dictionary<string, int>()
    {
      {JS.Name,1}, {TS.Name, 0}, {CL.Name, 0}, {CD.Name, 1}
    };
    YJ.Order(Ord1, HN);
    HN.CookOrder(AB.FoodOrder, AB);
    AB.Cook(AB.FoodOrder, HN);
    HN.Serve(HN.Hand, YJ);
    YJ.Pay(YJ.FoodOrder, HN);


    MyCustomer MJ = new MyCustomer("Miju");

    MyCustomer HJ = new MyCustomer("Hyejeong");
    Dictionary<string, int> Ord2 = new Dictionary<string, int>()
    {
      {JS.Name, 0}, {TS.Name, 1}, {CL.Name, 1}, {CD.Name, 0}
    };
    Dictionary<string, int> Ord3 = new Dictionary<string, int>()
    {
      {JS.Name, 0}, {TS.Name, 1}, {CL.Name, 1}, {CD.Name, 0}
    };
    MJ.Order(Ord2, HN);
    HJ.Order(Ord3, HN);
    HN.CookOrder(AB.FoodOrder, AB);
    AB.Cook(AB.FoodOrder, HN);
    HN.Serve(HN.Hand, MJ);
    HN.Serve(HN.Hand, HJ);
    MJ.Pay(MJ.FoodOrder, HN);
    HJ.Pay(HJ.FoodOrder, HN);

    HN.GiveMoney(JB);
    JB.GiveSalary(HN);
    JB.GiveSalary(AB);
    JB.CloseBusiness();
  }
}


public class MyOwner
{
  public string Name;
  public int Money;

  public MyOwner(string OwnerName)
  {
    Name = OwnerName;
    Money = 1000000;
  }

  public void GiveSalary(MyWorker worker)
  {
    Console.WriteLine($"식당주인 {Name}는 종업원 {worker.Name}에게 월급 50000원을 지불한다");
    Money -= 50000;
    (worker.Salary) += 50000;
  }

  public void CloseBusiness()
  {
    Console.WriteLine($"사장 {Name}가 가진 {Money}원으로 가게 마감");
  }
};

public class MyWorker
{
  public string Name;
  public int Salary;

  public MyWorker()
  {
    Name = "";
    Salary = 0;
  }
};

public class MyCook : MyWorker
{
  public Dictionary<string, int> FoodOrder = new Dictionary<string, int>();

  public MyCook(string CookName)
  {
    this.Name = CookName;
    this.Salary = 0;
  }

  public void Cook(Dictionary<string, int> SFoodOrder, MyServer server)
  {
    Console.WriteLine($"요리담당 {Name}이 요리를 서빙담당 {server.Name}에게 준다");
    server.Hand = SFoodOrder;
  }
};

public class MyServer : MyWorker
{
  public Dictionary<string, int> FoodOrder = new Dictionary<string, int>();
  public Dictionary<string, int> MPrice = new Dictionary<string, int>();
  public Dictionary<string, int> Hand = new Dictionary<string, int>();
  public int Price;
  public int MoneyToday;

  public MyServer(string ServerName)
  {
    this.Name = ServerName;
    this.Salary = 0;
    Price = 0;
    MoneyToday = 0;
  }

  public void CookOrder(Dictionary<string, int> FoodOrd, MyCook cook)
  {
    Console.WriteLine($"서빙담당 {Name}이 요리담당 {cook.Name}에게 요리를 시킨다");
    cook.FoodOrder = FoodOrd;
    foreach (KeyValuePair<string, int> kv in FoodOrder)
    {
      FoodOrder[kv.Key] = 0;
    }
  }

  public void Serve(Dictionary<string, int> FoodOrd, MyCustomer customer)
  {
    Console.WriteLine($"서빙담당 {Name}이 요리를 손님 {customer.Name}에게 건넨다");
    customer.Table = Hand;
  }

  public void GiveMoney(MyOwner owner)
  {
    Console.WriteLine($"서빙담당 {Name}이 식당주인 {owner.Name}에게 오늘 매출 {MoneyToday}원을 준다");
    owner.Money += MoneyToday;
  }
};


public class MyCustomer
{
  public string Name;
  public Dictionary<string, int> FoodOrder = new Dictionary<string, int>();
  public Dictionary<string, int> Table = new Dictionary<string, int>();
  public int Price;
  public Dictionary<string, int> MenuPrice = new Dictionary<string, int>();

  public MyCustomer(string CustomerName)
  {
    Name = CustomerName;

  }

  public void Order(Dictionary<string, int> Ord, MyServer server)
  {
    Console.WriteLine($"손님 {Name}이 음식을 주문했다.");
    FoodOrder = Ord;
    foreach (KeyValuePair<string, int> kv in FoodOrder)
    {
      server.FoodOrder[kv.Key] += kv.Value;
    }
  }

  public void Pay(Dictionary<string, int> FoodOrder, MyServer server)
  {
    MenuPrice = server.MPrice;
    Price = 0;
    foreach (KeyValuePair<string, int> kv in FoodOrder)
    {
      Price += (kv.Value) * MenuPrice[kv.Key];
    }
    Console.WriteLine($"손님 {Name}은 서빙담당 {server.Name}에게 {Price}원을 준다.");
    server.MoneyToday += Price;
  }
};

public class MyMenu
{
  public string Name;
  public int Price;

  public MyMenu(string OwnerName, int MenuPrice)
  {
    Name = OwnerName;
    Price = MenuPrice;
  }
  public void MakeMPrice(MyServer server)
  {
    server.FoodOrder[Name] = 0;
    server.MPrice[Name] = Price;
  }
};


