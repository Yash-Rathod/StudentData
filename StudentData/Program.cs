using StudentData.Classes;
using System.Text.Json;

Console.WriteLine(":::::::::::::::::::::::::::");
Console.Write(":::::     WELCOME     :::::\n");
Console.WriteLine(":::::::::::::::::::::::::::\n\n");


int choice;
string name;
int mainFlag = 1;
bool isLoggedin = false;
string filePath = Path.Combine(Environment.CurrentDirectory, "studentInformation.json");
FileInfo fileInfo = new FileInfo(filePath);
//Console.WriteLine(Environment.CurrentDirectory);
string studentInformation;
string serializedCode;

List<StudentInfo> studentList = new List<StudentInfo>();

// File Operations

if (!File.Exists(filePath))
{
    File.Create(filePath);
    //Console.WriteLine("File Created");
}
else
{
    studentInformation = File.ReadAllText(filePath);
    //Console.WriteLine("File Opened");

    if (fileInfo.Length != 0)
    {
        studentList = JsonSerializer.Deserialize<List<StudentInfo>>(studentInformation);
    }
}
 
while(mainFlag != 0)
{     
    Console.Write("Please Enter your selection from below list.\n\n");
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Register");
    Console.WriteLine("0. Exit");
    Console.Write("\nEnter Number :: ");            

    choice = Convert.ToInt32(Console.ReadLine());
    if (choice != 0)
    {
        // REGISTER USER
        if (choice == 2)
        {
            // Getting Student Info

            Console.Write("\n\n::::: STUDENT REGISTERATION :::::\n\n");

            StudentInfo student = new StudentInfo();
            Console.Write("Enter Student ID ::");
            student.Id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Name :: ");
            student.Name = Console.ReadLine();
            Console.Write("Enter Email :: ");
            student.Email = Console.ReadLine();
            Console.Write("Enter Password :: ");
            student.Password = Console.ReadLine();
            Console.Write("Enter your Age :: ");
            student.Age = Convert.ToInt32(Console.ReadLine());
            studentList.Add(student);

            Console.Write("\n\n::::: STUDENT REGISTERED SUCCESSFULLY! :::::\n\n");

            // Writing to file and redirecting to Main Menu
            var options = new JsonSerializerOptions { WriteIndented = true };
            serializedCode = JsonSerializer.Serialize(studentList, options);
            File.WriteAllText(filePath, serializedCode);
            //Console.WriteLine(studentList.Count.ToString());
        }

        // LOG IN
        else if (choice == 1 && isLoggedin == false)
        {
            int loginFlag = 1;
            string pass;
            int logOutCheck;
            int idCheck = 0;
            int found = 1;
            while (loginFlag != 0)
            {
                Console.Write("\n\n::::: STUDENT LOGIN :::::\n\n");
                Console.Write("To go back to main menu enter \"Exit\"\n");
                Console.Write("Enter Student Name :: ");
                name = Console.ReadLine();
                if(name == "Exit" || name == "exit" || name=="EXIT")
                {
                    loginFlag = 0;
                }
                else
                {
                    for (int i = 0; i < studentList.Count; i++)
                    {

                        if (studentList[i].Name == name)
                        {
                            Console.Write("Enter your Password :: ");
                            found = 0;
                            pass = Console.ReadLine();
                            if (studentList[i].Password == pass)
                            {
                                Console.WriteLine("You have logged in successfully!");
                                Console.WriteLine("To view previously saved data enter \"1\" :: ");
                                Console.WriteLine("To Log Out press \'0\'");
                                isLoggedin = true;
                                int view = Convert.ToInt32(Console.ReadLine());
                                if (view == 1)
                                {
                                    Console.Write("\n\n     :::::     STUDENT DATA     :::::     \n\n");
                                    Console.Write("\t\tID\t\tName\t\tEmail\t\tPassword\t\tAge\n");
                                    for (int j = 0; j < studentList.Count; j++)
                                    {
                                        Console.WriteLine("\t\t{0}\t\t{1}\t\t{2}\t\t{3}", studentList[j].Id, studentList[j].Name, studentList[j].Email, studentList[j].Age);
                                    }
                                    Console.WriteLine("\n\n");
                                }
                                else if (view == 0)
                                {
                                    isLoggedin = false;
                                    loginFlag = 0;
                                }

                                // Log Out

                                if(view != 0)
                                {
                                    Console.Write("Press \'0\' to Logout :: ");
                                    logOutCheck = Convert.ToInt32(Console.ReadLine());

                                    if (logOutCheck == 0)
                                    {
                                        isLoggedin = false;
                                        loginFlag = 0;
                                    }
                                }                                
                            }
                            else
                            {
                                Console.WriteLine("Incorrect Password!");
                            }

                        }
                        else
                        {
                            idCheck = 1;
                        }
                    }

                    if (idCheck == 1 && found == 1)
                    {
                        Console.WriteLine("Student Name not registered!");
                        Console.WriteLine("Redirecting to Main Menu");
                        loginFlag = 0;
                    }
                }                
            }
        }
    }
    else
    {
        mainFlag = choice;
    }
}

Console.Write("\n\n:::::::::::::::::::::::::::::\n");
Console.Write(":::::     THANK YOU     :::::\n");
Console.Write(":::::::::::::::::::::::::::::\n\n");