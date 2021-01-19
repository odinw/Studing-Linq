using System;
using System.Collections.Generic;
using System.Linq;

namespace Studing_Linq
{
    class Member
    {
        public long uid;
        public string account;
    }

    class Program
    {
        static void Main(string[] args)
        {
            //IEnumrableConcat();
            Class_Where_Select_Member();
            //DateTimeOffset_Get_Offset();
            //Set();
            //Fail_Where_Set();
            //Distinct();
            //TakeWhile();
        }

        /// <summary>
        /// 把兩個 IEnumrable 成員合併，相當於 List.AddRange()
        /// </summary>
        static void IEnumrableConcat()
        {
            IEnumerable<Member> members1 = new List<Member>
            {
                new Member { uid = 1, account = "A" },
                new Member { uid = 2, account = "B" },
            };

            IEnumerable<Member> members2 = new List<Member>
            {
                new Member { uid = 3, account = "C" },
            };

            var member3 = members1.Concat(members2);

            Console.WriteLine("members1");
            members1.ToList().ForEach(x => Console.WriteLine(x.uid));
            Console.WriteLine("members2");
            members2.ToList().ForEach(x => Console.WriteLine(x.uid));
            Console.WriteLine("members3");
            member3.ToList().ForEach(x => Console.WriteLine(x.uid));
        }

        /// <summary>
        /// 修改 IEnumerable 成員，加上 ToList() 就會改變
        /// </summary>
        static void Class_Where_Select_Member()
        {
            IEnumerable<Member> members = new List<Member>
            {
                new Member { uid = 1, account = "A" },
                new Member { uid = 2, account = "B" },
                new Member { uid = 3, account = "C" },
            };

            Console.WriteLine("Before members");
            members.ToList().ForEach(x => Console.WriteLine(x.uid));

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
                if (member.account == "B")
                    member.uid = 5;
                return true;
            });
            Console.WriteLine("After members");
            members.ToList().ForEach(x => Console.WriteLine(x.uid)); // 變成 1, 5, 3
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
