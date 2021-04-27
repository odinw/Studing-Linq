using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Studing_Linq
{
    class Member
    {
        public long number;
        public string name;
    }

    class Program
    {
        static void Main(string[] args)
        {
            NullConditionalOperator();
            //Any();
            //DynamicType();
            //DistinctClass();
            //IEnumrableConcat();
            //Class_Where_Select_Member();
            //DateTimeOffset_Get_Offset();
            //Set();
            //Fail_Where_Set();
            //Distinct();
            //TakeWhile();
        }

        /// <summary>
        /// 空條件運算符 null conditional operator  ：以 List?ToDo 取代 if (List == null) ToDo
        /// ref :
        /// https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/october/csharp-the-new-and-improved-csharp-6-0#null-conditional-operator
        /// https://stackoverflow.com/questions/24390005/checking-for-empty-or-null-liststring
        /// </summary>
        static void NullConditionalOperator()
        {
            List<int> SomethingList = new List<int> { 1, 2, 3 };
            List<int> ZoreList = new List<int>();
            List<int> nullList = null;

            Console.WriteLine($"nullList?.Any() ?? false = {nullList?.Any() ?? true}");
            Console.WriteLine(nullList?.Count ?? -1); // if nullList is null, return -1. Testing: -1
            Console.WriteLine(ZoreList?.Count ?? -1); // Testing: 0
            Console.WriteLine(SomethingList?.Count ?? -1); // Testing: 3

            // Testing: false
            if (nullList?.Any() ?? false)
                Console.WriteLine(true);
            else
                Console.WriteLine(false);
        }

        /// <summary>
        /// 是否有元素
        /// </summary>
        static void Any()
        {
            List<int> SomethingList = new List<int> { 1, 2, 3};
            List<int> ZoreList = new List<int>();
            List<int> nullList = null;

            Console.WriteLine(SomethingList.Any()); // true
            Console.WriteLine(ZoreList.Any()); // false
            //Console.WriteLine(nullList.Any()); // System.ArgumentNullException: 'Value cannot be null. '
            Console.WriteLine(nullList?.Any()); // 沒東西，變成只是分行
        }

        /// <summary>
        /// 研究 dynamic 型別，設定初始值並傳給 ShowDynamic() 使用
        /// </summary>
        static void DynamicType()
        {
            dynamic body = new
            {
                nickname = "Jack",
                age = 20
            };

            //body.Add( new { tall = 175 }); // Microsoft.CSharp.RuntimeBinder.RuntimeBinderException: ''<>f__AnonymousType0<string,int>' does not contain a definition for 'Add''

            ShowDynamic(body);
        }

        /// <summary>
        /// 顯示 dynamic 值
        /// </summary>
        /// <param name="body"></param>
        static void ShowDynamic(dynamic body)
        {
            Console.WriteLine(body.nickname);
            Console.WriteLine(body.age);
        }

        /// <summary>
        /// 把兩個 IEnumrable 成員合併，相當於 List.AddRange()
        /// </summary>
        static void IEnumrableConcat()
        {
            IEnumerable<Member> members1 = new List<Member>
            {
                new Member { number = 1, name = "A" },
                new Member { number = 2, name = "B" },
            };

            IEnumerable<Member> members2 = new List<Member>
            {
                new Member { number = 3, name = "C" },
            };

            var member3 = members1.Concat(members2);

            Console.WriteLine("members1");
            members1.ToList().ForEach(x => Console.WriteLine(x.number));
            Console.WriteLine("members2");
            members2.ToList().ForEach(x => Console.WriteLine(x.number));
            Console.WriteLine("members3");
            member3.ToList().ForEach(x => Console.WriteLine(x.number));
        }

        /// <summary>
        /// 修改 IEnumerable 成員，加上 ToList() 就會改變
        /// </summary>
        static void Class_Where_Select_Member()
        {
            IEnumerable<Member> members = new List<Member>
            {
                new Member { number = 1, name = "A" },
                new Member { number = 2, name = "B" },
                new Member { number = 3, name = "C" },
            };

            Console.WriteLine("Before members");
            members.ToList().ForEach(x => Console.WriteLine(x.number));

            // 1. members 值不會改變
            //var updateB = members.Where(member => member.account == "B").Select(member => { member.uid = 5; return member; });
            //Console.WriteLine("After members");
            //members.ToList().ForEach(x => Console.WriteLine(x.uid)); // 仍然是 1, 2, 3
            //Console.WriteLine("updateB");
            //updateB.ToList().ForEach(x => Console.WriteLine(x.uid)); // 5

            // 2. 有 ToList，members 就會改變
            //var updateB = members.Where(member => member.account == "B").Select(member => { member.uid = 5; return member; }).ToList();
            //Console.WriteLine("After members");
            //members.ToList().ForEach(x => Console.WriteLine(x.uid)); // 變成 1, 5, 3
            //Console.WriteLine("updateB");
            //updateB.ToList().ForEach(x => Console.WriteLine(x.uid)); // 5

            // 3. 於 Where 中修改並加上 ToList() 也會改變 members
            //var updateB = members.Where(member =>
            //{
            //    if (member.account == "B")
            //    { 
            //        member.uid = 5;
            //        return true;
            //    }
            //    return false;
            //}).ToList();

            // 4. 用把自己指派為新值，就會改變 members
            members = members.Where(member =>
            {
                if (member.name == "B")
                    member.number = 5;
                return true;
            });
            Console.WriteLine("After members");
            members.ToList().ForEach(x => Console.WriteLine(x.number)); // 變成 1, 5, 3
        }

        /// <summary>
        /// ing 時區 研究中
        /// </summary>
        static void DateTimeOffset_Get_Offset()
        {
            //DateTimeOffset dto = DateTimeOffset.Now;
            DateTimeOffset dto = DateTimeOffset.Parse("2021-1-1 00:00:00-08:00");


            var a = dto.Offset;
            var toString = dto.Offset.ToString();
            var Offset_Hours = dto.Offset.Hours;
            Console.WriteLine(a);
            Console.WriteLine(a.ToString());
            Console.WriteLine(toString);
            Console.WriteLine(Offset_Hours);

            TimeZoneInfo timeZoneInfo = TimeZoneInfo.Local;

            var aa = dto.DateTime;
            var datetimeKind = dto.DateTime.Kind;


            //TimeZone localZone = TimeZone.CurrentTimeZone;
        }

        static void Fail_Where_Set()
        {
            List<int> data = new List<int> { 1, 2, 3, 4, 5 };

            Console.WriteLine("before");
            data.ForEach(item => Console.WriteLine(item));

            var result = data.Where(item =>
            {
                if (item < 3)
                {
                    item = 666;
                    return true;
                }
                return false;
            }).ToList();
            Console.WriteLine("after data");
            data.ForEach(item => Console.WriteLine(item));
            Console.WriteLine("result");
            result.ForEach(item => Console.WriteLine(item));
            Console.WriteLine("after data");
            data.ForEach(item => Console.WriteLine(item));
        }

        static void Distinct()
        {
            IEnumerable<int> data = new int[] { 1, 1, 1, 2, 3 };

            Console.WriteLine("before");
            data.ToList().ForEach(item => Console.WriteLine(item));

            Console.WriteLine("after");
            data.Distinct().ToList().ForEach(item => Console.WriteLine(item)); // 1, 2, 3
        }

        /// <summary>
        /// Take (from the front) & TakeLast
        /// </summary>
        static void Take_TakeLast()
        {
            IEnumerable<int> list = new List<int> { 1, 2, 3, 4, 5 };
            var top = list.Take(2);
            var last = list.TakeLast(2);
            top.ToList().ForEach(v => Console.WriteLine($"top {v}"));
            last.ToList().ForEach(v => Console.WriteLine($"last {v}"));
        }

        // ing
        static void DistinctClass()
        {
            IEnumerable<Member> data = new List<Member>
            {
                new Member { number = 1, name = "A" },
                new Member { number = 2, name = "B" },
                new Member { number = 2, name = "C" },
                new Member { number = 4, name = "C" },
            };




            Console.WriteLine("before");
            data.ToList().ForEach(item => Console.WriteLine(item.number));

            Console.WriteLine("after");
            var a = data.GroupBy(x => x.number);//.ToList().ForEach(item => Console.WriteLine(item.number));
            var json = JsonConvert.SerializeObject(a, Formatting.Indented);
            Console.WriteLine(json);
        }

        /// <summary>
        /// TakeWhile: 持續複製元素，直到指定條件為 true 就停止複製
        /// </summary>
        static void TakeWhile()
        {
            // reference: https://docs.microsoft.com/zh-tw/dotnet/api/system.linq.enumerable.takewhile?view=net-5.0
            string[] fruits = { "apple", "banana", "mango", "passionfruit", "orange", "grape" };

            // before
            fruits.ToList().ForEach(item => Console.WriteLine(item));
            Console.WriteLine();

            // after
            IEnumerable<string> query = fruits.TakeWhile(fruit => String.Compare("orange", fruit, true) != 0);
            query.ToList().ForEach(item => Console.WriteLine(item));
            Console.WriteLine();
            fruits.ToList().ForEach(item => Console.WriteLine(item));
        }

    }
}
