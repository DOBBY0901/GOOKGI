using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Practice3
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int MathGrade { get; set; }
        public int EnglishGrade { get; set; }

        public Student(string _name, int _age, int _mathGrade, int _englishGrade)
        {
            Name = _name;
            Age = _age;
            MathGrade = _mathGrade;
            EnglishGrade = _englishGrade;
        }
        
        //학생정보
        public void StudentInfo()
        {
            Console.WriteLine();
            Console.Write($"이름 : {Name}, 나이 : {Age}, 수학점수 : {MathGrade}, 영어점수 : {EnglishGrade}");
            Console.WriteLine();
        }

        //합격유무
        public bool IsPassed()
        {
            return (MathGrade >= 80 || EnglishGrade >= 80);
        }

        

    }
     
    public class Program
    {
        //학생 정보 출력
        static void PrintAllStudent(List<Student> studentsList)
        {
                Console.Clear();
                Console.WriteLine("[전체 학생 출력]");
                Console.WriteLine();

            for (int i = 0; i < studentsList.Count; i++)
            {
                studentsList[i].StudentInfo();
            }
                
                Console.ReadKey();
        }
        
        //합격 유무 출력
        static void PrintPassed(List<Student> studentsList)
        {
            Console.Clear();
            Console.WriteLine("[합격 유무]");
            Console.WriteLine();

            for(int i = 0;i < studentsList.Count; i++)
            {
                if (studentsList[i].IsPassed())
                {
                    Console.WriteLine($"{studentsList[i].Name} : 합격");
                }

                else
                {
                    Console.WriteLine($"{studentsList[i].Name} : 불합격");
                }
            }

            Console.ReadKey();
        }

        //평균 출력
        static void PrintAverage(List<Student> studentsList)
        {
            double totalMath = 0;
            double totalEng = 0;

            Console.Clear();
            Console.WriteLine("[평균 출력]");
            Console.WriteLine();

            for (int i = 0; i < studentsList.Count; i++)
            {
                totalMath += studentsList[i].MathGrade;
                totalEng += studentsList[i].EnglishGrade;
            }

            double mathAverage = totalMath / studentsList.Count;
            double engAverage = totalEng / studentsList.Count;
            double entireAverage = (totalMath + totalEng) / (studentsList.Count * 2);

           
            Console.WriteLine($"수학 점수 평균 : {mathAverage:F1}");
            Console.WriteLine($"영어 점수 평균 : {engAverage:F1}");
            Console.WriteLine($"전체 점수 평균 : {entireAverage:F1}");

            Console.ReadKey();
        }

        //학생 추가
        static void AddStudent(List<Student> studentsList)
        {
            Console.Clear();
            Console.WriteLine("[학생 추가]");

            Console.Write("이름 : ");
            string name = Console.ReadLine();

            Console.Write("나이 : ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("수학 점수 : ");
            int math = int.Parse(Console.ReadLine());

            Console.Write("영어 점수 : ");
            int eng = int.Parse(Console.ReadLine());

            studentsList.Add(new Student(name, age, math, eng));

            Console.WriteLine($"{name} 학생이 추가되었습니다.");
            Console.ReadKey();
              
        }

        //학생 삭제
        static void RemoveStudent(List<Student> studentsList)
        {
            Console.Clear();
            Console.WriteLine("[학생 삭제]");  
            Console.WriteLine();
            Console.WriteLine("삭제하려는 학생의 이름을 입력해주세요.");
            
            for (int i = 0; i < studentsList.Count; i++)
            {
                studentsList[i].StudentInfo();
            }

            Console.WriteLine();
            string name = Console.ReadLine();
            for (int i = 0; i < studentsList.Count; i++)
            {
                if (studentsList[i].Name == name)
                {
                    studentsList.RemoveAt(i);
                    Console.WriteLine($"{name} 학생이 삭제되었습니다.");
                    Console.ReadKey();
                    return;
                }
            }
             
            Console.ReadKey();
        }

        // 평균 이상인 학생만 출력
        static void PrintAverageStudent(List<Student> studentsList)
        {
            double totalMath = 0;
            double totalEng = 0;

            for (int i = 0; i < studentsList.Count; i++)
            {
                totalMath += studentsList[i].MathGrade;
                totalEng += studentsList[i].EnglishGrade;
            }

            double mathAverage = totalMath / studentsList.Count;
            double engAverage = totalEng / studentsList.Count;
            double entireAverage = (totalMath + totalEng) / (studentsList.Count * 2);
            
            Console.Clear();
            Console.WriteLine("[평균 이상 학생 출력]");
            Console.WriteLine();
            Console.WriteLine($"수학평균 이상 학생 :");

            for (int i = 0; i < studentsList.Count; i++)
            {
                if (studentsList[i].MathGrade >= mathAverage)
                {
                    Console.Write($"'{studentsList[i].Name}' ");
                }
            }
            Console.WriteLine();
            Console.WriteLine($"영어평균 이상 학생 :");
            for (int i = 0; i < studentsList.Count; i++)
            {
                if (studentsList[i].EnglishGrade >= engAverage)
                {
                    Console.Write($"'{studentsList[i].Name}' ");
                }
            }

            Console.ReadKey ();
        }
        static void Main()
        {
            List<Student> studentsList = new List<Student>();

            studentsList.Add(new Student("김씨", 25, 90, 95));
            studentsList.Add(new Student("이씨", 30, 88, 68));
            studentsList.Add(new Student("박씨", 35, 100, 97));
            studentsList.Add(new Student("최씨", 40, 65, 59));
            studentsList.Add(new Student("정씨", 45, 61, 78));


            while (true)
            {
                Console.Clear();
                Console.WriteLine("학생 관리 배열");
                Console.WriteLine();
                Console.WriteLine("1. 전체 학생 출력");
                Console.WriteLine("2. 합격 유무 출력");
                Console.WriteLine("3. 평균 출력");
                Console.WriteLine("4. 학생 추가");
                Console.WriteLine("5. 학생 삭제");
                Console.WriteLine("6. 점수가 평균 이상인 학생만 출력");
                Console.WriteLine("0. 종료");
                Console.WriteLine();

                ConsoleKeyInfo consolekeyinfo = Console.ReadKey();
                switch(consolekeyinfo.Key)
                {
                    case ConsoleKey.D0:
                        return;

                    case ConsoleKey.D1:
                        PrintAllStudent(studentsList);
                        break;

                    case ConsoleKey.D2:
                        PrintPassed(studentsList);
                        break;

                    case ConsoleKey.D3:
                        PrintAverage(studentsList);
                        break;

                    case ConsoleKey.D4:
                        AddStudent(studentsList);
                        break;
                   
                    case ConsoleKey.D5:
                        RemoveStudent(studentsList);
                        break;

                    case ConsoleKey.D6:
                        PrintAverageStudent(studentsList);
                        break;

                    default:
                        break;
                }
                
            }

        }
    }
}

/*
 * 먼저 배열로 만들고
 * 1. 학생을 관리하는 배열
 * 2. 점수 배열 저장
 * 3. 평균 출력
 * 4. 합격 / 불합격 분류
 * 5. Student 클래스 도입
 * 
 * 여기서 리스트로 변환
 * 1. List<student> 변경
 * 2. 학생 추가
 * 3. 학생 삭제
 * 4. 전체 출력
 * 5. 평균 이상만 출력
 * 6. 최고 점수 학생 출력
 * 7. 메뉴 통합
 * 8. 리팩토링
  */
