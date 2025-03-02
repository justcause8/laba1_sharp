internal class Program
{
    static string[] students = new string[10];
    static string[] courses = new string[5];
    static bool[,] enrollments = new bool[5, 10];

    private static void Main(string[] args)
    {
        while (true)
        {
            ShowMenu();
            Console.Write("Выбрать опцию: ");

            string choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1": AddStudent(students); break;
                case "2": ShowList(students, "студентов"); break;
                case "3": AddCourse(courses); break;
                case "4": ShowList(courses, "курсов"); break;
                case "5": AddStudentOnCourse(students, courses); break;
                case "6": ShowCourseStudents(students, courses); break;
                case "7": RemoveItemFromArrays(students, "студент"); break;
                case "8": RemoveItemFromArrays(courses, "курс"); break;
                case "9": ShowCoursesAndStudents(students, courses, enrollments); break;
                case "10": DeleteStudentFromCourse(students, courses, enrollments); break;
                case "11": SearchStudent(students); break;
                case "0": return;
                default: Console.WriteLine("Ошибка"); break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\nМеню");
        Console.WriteLine("|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|");
        Console.WriteLine("1. Добавить студента");
        Console.WriteLine("2. Показать всех студентов");
        Console.WriteLine("3. Добавить курс");
        Console.WriteLine("4. Показать все курсы");
        Console.WriteLine("5. Записать студента на курс");
        Console.WriteLine("6. Показать студентов курса");
        Console.WriteLine("7. Удалить студента");
        Console.WriteLine("8. Удалить курс");
        Console.WriteLine("9. Показать все курсы и их студентов");
        Console.WriteLine("10.Снять студента с курса");
        Console.WriteLine("11.Найти студента");
        Console.WriteLine("0.Выход");
        Console.WriteLine("|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|");
    }

    static void AddEntity(string[] array, string entityType)
    {
        Console.Write($"Введите имя/название {entityType}: ");
        string name = Console.ReadLine();

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
            {
                array[i] = name;
                Console.WriteLine($" - {entityType} {name} добавлен.");
                return;
            }
        }

        Console.WriteLine($"Ошибка: Нет свободных мест для {entityType}.");
    }

    static void AddStudent(string[] students)
    {
        string entityType = "студент";
        AddEntity(students, entityType);
    }

    static void AddCourse(string[] courses)
    {
        string entityType = "курс";
        AddEntity(courses, entityType);
    }

    static void ShowList(string[] list, string entityType)
    {
        Console.WriteLine($"Список {entityType}:");
        bool hasItems = false;

        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] != null)
            {
                Console.WriteLine($"[{i}] {list[i]}");
                hasItems = true;
            }
        }

        if (!hasItems)
        {
            Console.WriteLine($"Нет {entityType}.");
        }
    }

    static int GetId(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int id) && id >= min && id <= max)
            {
                return id;
            }

            Console.WriteLine($"Ошибка: введите число от {min} до {max}.");
        }
    }

    static void AddStudentOnCourse(string[] students, string[] courses)
    {
        ShowList(students, "студентов");
        Console.WriteLine();

        int studentID = GetId("Введите ID студента: ", 0, students.Length - 1);
        Console.WriteLine();

        ShowList(courses, "курсов");
        Console.WriteLine();

        int courseID = GetId("Введите ID курса: ", 0, courses.Length - 1);

        if (students[studentID] != null && courses[courseID] != null)
        {
            enrollments[courseID, studentID] = true;
            Console.WriteLine($" - {students[studentID]} записан на курс {courses[courseID]}.");
        }
        else
        {
            Console.WriteLine("Ошибка: неверный ID студента или курса.");
        }
    }

    static void ShowCourseStudents(string[] students, string[] courses)
    {
        ShowList(courses, "курсов");
        Console.WriteLine();

        int courseID = GetId("Введите ID курса: ", 0, courses.Length - 1);

        if (courses[courseID] == null)
        {
            Console.WriteLine("Некорректный курс");
        }

        Console.WriteLine($"Список студентов курса: {courses[courseID]}");
        bool hasStudent = false;
        for (int i = 0; i < students.Length; i++)
        {
            if (enrollments[courseID, i] && students[i] != null)
            {
                Console.WriteLine("- " + students[i]);
                hasStudent = true;
            }
        }
        if (!hasStudent)
        {
            Console.WriteLine("На этот курс никто не записан");
        }
    }

    static void RemoveItemFromArrays(string[] array, string entityType)
    {
        if (array.All(item => item == null))
        {
            Console.WriteLine($"Нет {entityType} для удаления.");
            return;
        }

        ShowList(array, entityType);
        Console.WriteLine();

        int id = GetId($"Введите номер {entityType} для удаления: ", 0, array.Length - 1);

        if (array[id] == null)
        {
            Console.WriteLine($"Ошибка: выбранный {entityType} не существует.");
            return;
        }

        array[id] = null; // Удаляем элемент, зануляя его

        Console.WriteLine($"{entityType} успешно удален.");
    }


    static void ShowCoursesAndStudents(string[] students, string[] courses, bool[,] enrollments)
    {
        Console.WriteLine("Список курсов и их студентов:");

        for (int i = 0; i < courses.Length; i++)
        {
            if (courses[i] != null)
            {
                Console.WriteLine($"\nКурс: {courses[i]}");
                bool hasStudents = false;

                for (int j = 0; j < students.Length; j++)
                {
                    if (students[j] != null && enrollments[i, j])
                    {
                        Console.WriteLine($" - {students[j]}");
                        hasStudents = true;
                    }
                }

                if (!hasStudents)
                {
                    Console.WriteLine("   На этот курс никто не записан.");
                }
            }
        }
    }

    static void DeleteStudentFromCourse(string[] students, string[] courses, bool[,] enrollments)
    {
        ShowList(students, "студентов");
        Console.WriteLine();

        int studentID = GetId("Введите ID студента: ", 0, students.Length - 1);

        if (students[studentID] == null)
        {
            Console.WriteLine("Ошибка: выбранный студент не существует.");
            return;
        }

        Console.WriteLine($"\nКурсы, на которые записан {students[studentID]}:");

        List<int> enrolledCourses = new List<int>();

        for (int i = 0; i < courses.Length; i++)
        {
            if (courses[i] != null && enrollments[i, studentID])
            {
                Console.WriteLine($"[{i}] {courses[i]}");
                enrolledCourses.Add(i);
            }
        }

        if (enrolledCourses.Count == 0)
        {
            Console.WriteLine("Этот студент не записан ни на один курс.");
            return;
        }

        Console.WriteLine();
        int courseID = GetId("Введите ID курса для снятия: ", 0, courses.Length - 1);

        if (!enrolledCourses.Contains(courseID))
        {
            Console.WriteLine("Ошибка: студент не записан на этот курс.");
            return;
        }

        enrollments[courseID, studentID] = false;
        Console.WriteLine($"Студент {students[studentID]} успешно снят с курса {courses[courseID]}.");
    }

    static void SearchStudent(string[] students)
    {
        Console.Write("Введите имя студента для поиска: ");
        string searchName = Console.ReadLine().Trim();
        bool found = false;

        for (int i = 0; i < students.Length; i++)
        {
            if (students[i] != null && students[i].IndexOf(searchName, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Console.WriteLine($"Студент найден: ID [{i}], Имя: {students[i]}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Студент не найден.");
        }
    }
}