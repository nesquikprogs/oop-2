@startuml

class Student {
    +int Id
    +string Name
    +int Age
    +int Grade
    +string Email
}

class DatabaseManager {
    -string FilePath
    +void SetFilePath(path: string)
    +ObservableCollection<Student> LoadFromFile()
    +void SaveToFile(students: ObservableCollection<Student>)
}

interface IStudentRepository {
    +ObservableCollection<Student> LoadStudents()
    +void SaveStudents(students: ObservableCollection<Student>)
}

class MainViewModel {
    -DatabaseManager _db
    -Student _currentStudent
    -string _windowTitle
    -string _searchText

    +ObservableCollection<Student> Students
    +Student CurrentStudent
    +string WindowTitle
    +string Name
    +string AgeText
    +string GradeText
    +string Email
    +string SearchText

    +MainViewModel()
    +void SetFilePath(path: string)
    +void LoadStudents()
    +void AddStudent()
    +void UpdateStudent()
    +void DeleteStudent(student: Student)
    +void Search()
    +void SortByAge()
    +void Refresh()
}

class MainWindow {
    -MainViewModel _vm
    +MainWindow()
}

MainViewModel --> IStudentRepository : использует
MainViewModel --> DatabaseManager : использует
MainViewModel o-- Student : агрегация
MainWindow *-- MainViewModel : композиция
DatabaseManager ..|> IStudentRepository: реализует

@enduml
