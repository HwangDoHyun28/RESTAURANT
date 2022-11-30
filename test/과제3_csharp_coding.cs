HW3.MyRestaurant R = new HW3.MyRestaurant("JB");
HW3.MyRestaurant.MyOwner JB = new HW3.MyRestaurant.MyOwner("JB");
HW3.MyRestaurant.MyCook AB = new HW3.MyRestaurant.MyCook("Abyot");
HW3.MyRestaurant.MyServer HN = new HW3.MyRestaurant.MyServer("Hyun");
HW3.MyRestaurant.MyMenu JS = new HW3.MyRestaurant.MyMenu("정식", 5000);
HW3.MyRestaurant.MyMenu TS = new HW3.MyRestaurant.MyMenu("특식", 7000);
HW3.MyRestaurant.MyMenu CL = new HW3.MyRestaurant.MyMenu("콜라", 2000);
HW3.MyRestaurant.MyMenu CD = new HW3.MyRestaurant.MyMenu("사이다", 2000);

HW3.MyRestaurant.MyCustomer YJ = new HW3.MyRestaurant.MyCustomer("Yejin");


R.Customer();
HN.Order1();
List<string> Ord1 = new List<string>(new string[] { JS.Name, CD.Name });
YJ.Order(Ord1);

List<string> SFoodOrder1 = new List<string>(new string[] { JS.Name, CD.Name });
List<int> SFoodOrderNum1 = new List<int>(new int[] { 1, 1 });
List<int> SFoodOrderPrice1 = new List<int>(new int[] { JS.Price, CD.Price });

HN.Order2(SFoodOrder1, SFoodOrderNum1, SFoodOrderPrice1);

AB.Cook(HN.FoodOrder, HN.FoodOrderNum);
HN.Serve();
YJ.Eat();

YJ.Pay(HN.FoodOrderPrice.Sum());
HN.GetMoney((HN.FoodOrderPrice).Sum());

HW3.MyRestaurant.MyCustomer MJ = new HW3.MyRestaurant.MyCustomer("Miju");

R.Customer();
HW3.MyRestaurant.MyCustomer HJ = new HW3.MyRestaurant.MyCustomer("Hyejeong");

R.Customer();
HN.Order1();

List<string> Ord2 = new List<string>(new string[] { TS.Name, CL.Name });
List<string> Ord3 = new List<string>(new string[] { TS.Name, CL.Name });
MJ.Order(Ord1);
HJ.Order(Ord3);

List<string> SFoodOrder2 = new List<string>(new string[] { TS.Name, CL.Name });
List<int> SFoodOrderNum2 = new List<int>(new int[] { 2, 2 });
List<int> SFoodOrderPrice2 = new List<int>(new int[] { TS.Price, CL.Price });

HN.Order2(SFoodOrder2, SFoodOrderNum2, SFoodOrderPrice2);

AB.Cook(HN.FoodOrder, HN.FoodOrderNum);
HN.Serve();
MJ.Eat();
HJ.Eat();

MJ.Pay((HN.FoodOrderPrice[0] * HN.FoodOrderNum[0] +
       HN.FoodOrderPrice[1] * HN.FoodOrderNum[1]) / 2);
HJ.Pay((HN.FoodOrderPrice[0] * HN.FoodOrderNum[0] +
       HN.FoodOrderPrice[1] * HN.FoodOrderNum[1]) / 2);

HN.GetMoney((HN.FoodOrderPrice[0] * HN.FoodOrderNum[0] +
       HN.FoodOrderPrice[1] * HN.FoodOrderNum[1]));

HN.GiveMoney(HN.MoneyToday);
JB.GetTodayMoney(HN.MoneyToday);
JB.GiveSalary();
HN.GetSalary();
AB.GetSalary();

JB.TotalMoney();


namespace HW3
{
  class MyRestaurant
  {
    public string Name;
    public int People;

    public MyRestaurant(string OwnerName)
    {
      Name = OwnerName;
      People = 0;
    }

    public void Start()
    {
      Console.WriteLine("System: 오늘도 날이 밝았네요.");
      Console.WriteLine($"System: {Name}의 식당에 어서오세요!");
    }

    public void Customer()
    {
      People += 1;
      Console.WriteLine("Workers: 어서오세요:)");
      Console.WriteLine($"System: 지금까지 {People}명의 손님이 왔습니다.");
      if (People == 3)
      {
        Console.WriteLine("JB: 이번 손님을 마지막으로 식당을 마감하도록 하겠습니다. 더 많이 오면 귀찮거든요. 인생은 선착순입니다.");
        Console.WriteLine("Workers: 아이고 사장님...");
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
      public void Start()
      {
        Console.WriteLine($"{Name}: 제가 이 식당의 주인 {Name}입니다, 빠른 테이블 회전을 바랍니다:)");
      }

      public void GetTodayMoney(int TodayMoney)
      {
        Console.WriteLine($"{Name}: 수고했습니다. 오늘도 적자로군요.");
        Money += TodayMoney;
      }

      public void GiveSalary()
      {
        Console.WriteLine($"{Name}: 여기 일급입니다. 조만간 폐업할테니 마음의 준비를 하세요.");
        Money -= 100000;
      }

      public void TotalMoney()
      {
        Console.WriteLine($"System: 그렇게 {Name}의 손에 남은 것은 {Money}원 뿐이었다...");
      }
    };

    public class MyWorker
    {
      public string Name;
      public int Money;

      public MyWorker()
      {
        Name = "who";
        Money = 0;
      }

      public virtual void Start()
      {
        Console.WriteLine($"Worker: 저는 이 식당의 종업원 {Name}입니다.");
      }

      public void GetSalary()
      {
        Money += 50000;
        Console.WriteLine($"System: {Name}의 일급이 임금되었습니다!");
        Console.WriteLine($"{Name}: 그럼 내일 뵙겠습니다, 사장님!");
      }


    };

    public class MyCook : MyWorker
    {
      public List<string> FoodOrder = new List<string>();
      public List<int> FoodOrderNum = new List<int>();
      public List<string> FoodList = new List<string>();

      public MyCook(string CookName)
      {
        this.Name = CookName;
        this.Money = 0;
      }

      public override void Start()
      {
        Console.WriteLine($"System: 이 식당의 종업원 {Name}입니다.");
        Console.WriteLine($"{Name}: 저는 cook입니다. 되도록이면 English로 말해주세요:)");
      }

      public void Cook(List<string> SFoodOrder, List<int> SFoodOrderNum)
      {
        Console.WriteLine($"{Name}: Okay, {SFoodOrder[0]} {SFoodOrderNum[0]}개, {SFoodOrder[1]} {SFoodOrderNum[1]}개!!");
        FoodOrder.Add(SFoodOrder[0]);
        FoodOrder.Add(SFoodOrder[1]);
        FoodOrderNum.Add(SFoodOrderNum[0]);
        FoodOrderNum.Add(SFoodOrderNum[1]);
        Console.WriteLine($"System: {Name}이 주문 받은 음식을 요리하고 있습니다.");
        FoodList.Add(SFoodOrder[0]);
        FoodList.Add(SFoodOrder[1]);
        Console.WriteLine($"{Name}: Done!!");
      }
    };

    public class MyServer : MyWorker
    {
      public List<string> FoodOrder = new List<string>();
      public List<int> FoodOrderNum = new List<int>();
      public List<int> FoodOrderPrice = new List<int>();
      public int Price;
      public int MoneyToday;

      public MyServer(string ServerName)
      {
        this.Name = ServerName;
        this.Money = 0;
        Price = 0;
        MoneyToday = 0;
      }

      public override void Start()
      {
        Console.WriteLine($"System: 이 식당의 종업원 {Name}입니다.");
        Console.WriteLine($"{Name}: 저는 웨이터입니다. 제게는 절대 영어로 말하지 마세요. 못 알아듣습니다:)");
      }

      public void Order1()
      {
        Console.WriteLine($"{Name}: 주문하시겠어요?");
      }

      public void Order2(List<string> SFoodOrder, List<int> SFoodOrderNum, List<int> SFoodOrderPrice)
      {
        FoodOrder = SFoodOrder;
        FoodOrderNum = SFoodOrderNum;
        FoodOrderPrice = SFoodOrderPrice;
        Console.WriteLine($"{Name}: 네, {FoodOrder[0]} {FoodOrderNum[0]}개, {FoodOrder[1]} {FoodOrderNum[1]}개, 주문받았습니다!");
        Console.WriteLine($"{Name}: Hey, {FoodOrder[0]} {FoodOrderNum[0]}개, {FoodOrder[1]} {FoodOrderNum[1]}개!!");
      }

      public void Serve()
      {
        Console.WriteLine($"{Name}: Thanks!!");
        Console.WriteLine("(Serving...)");
        Console.WriteLine($"{Name}: 주문하신 {FoodOrder[0]} {FoodOrderNum[0]}개, {FoodOrder[1]} {FoodOrderNum[1]}개 나왔습니다!");
      }

      public void GetMoney(int GetPrice)
      {
        Price += GetPrice;
        MoneyToday += GetPrice;
        Console.WriteLine($"{Name}: 네, 결제되었습니다! 감사합니다, 조심히 가세요!!");
      }

      public void GiveMoney(int TodayMoney)
      {
        Console.WriteLine($"{Name}: 사장님, 오늘 매출입니다! {TodayMoney}원이 나왔어요!!");
      }
    };


    public class MyCustomer
    {
      public string Name;
      List<string> Food = new List<string>();

      public MyCustomer(string CustomerName)
      {
        Name = CustomerName;

      }
      public void Start()
      {
        Console.WriteLine($"System: 딩동! 손님 {Name}(이/가) 왔습니다.");
      }

      public void Order(List<string> Ord)
      {
        string f1 = Ord[0];
        Food.Add(f1);
        Console.WriteLine($"{Name}: {f1} 주시고요,");
        string f2 = Ord[1];
        Food.Add(f2);
        Console.WriteLine($"{Name}: {f2}도 주세요.");
      }

      public void Eat()
      {
        Console.WriteLine($"{Name}: 감사합니다!");
        Console.WriteLine("(먹는 중...)");
      }

      public void Pay(int Price)
      {
        Console.WriteLine($"{Name}: 계산이요!!");
        Console.WriteLine($"{Name}: {Price}원 맞지요?");
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
      public void Start()
      {
        Console.WriteLine($"System: 이 식당의 메뉴로는 {Name}(이/가) 있습니다.");
        Console.WriteLine($"System: {Name}의 가격은 {Price}원 입니다.");
      }
    };

  };
};

