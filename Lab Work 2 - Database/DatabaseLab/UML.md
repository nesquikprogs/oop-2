@startuml
interface IStudentRepository {
    + AddStudent(student: Student)
    + UpdateStudent(student: Student)
    + DeleteStudent(id: int)
    + GetAllStudents(): List<Student>
    + SearchByName(searchText: string): List<Student>
    + SortByAge(): List<Student>
}

class Student {
    - Id: int
    - Name: string
    - Age: int
    - Grade: int
    - Email: string
}

class DatabaseManager {
    - _students: List<Student>
    - _nextId: int
    - FileName: const string
    + AddStudent(student: Student)
    + UpdateStudent(student: Student)
    + DeleteStudent(id: int)
    + GetAllStudents(): List<Student>
    + SearchByName(searchText: string): List<Student>
    + SortByAge(): List<Student>
    - LoadFromFile()
    - SaveToFile()
}

DatabaseManager ..|> IStudentRepository: реализует

class MainViewModel {
    - _db: IStudentRepository
    - _currentStudent: Student
    - _searchText: string
    + Students: ObservableCollection<Student>
    + CurrentStudent: Student
    + Name: string
    + AgeText: string
    + GradeText: string
    + Email: string
    + SearchText: string
    + AddStudent()
    + UpdateStudent()
    + DeleteStudent(student: Student)
    + Search()
    + SortByAge()
    + Refresh()
    - ValidateCurrentStudent(out error: string): bool
    - ClearCurrentStudent()
    + PropertyChanged: event
}

class MainWindow {
    - _vm: MainViewModel
    + MainWindow()
    + Add_Click(sender, e)
    + Update_Click(sender, e)
    + Delete_Click(sender, e)
    + Search_Click(sender, e)
    + Sort_Click(sender, e)
    + Refresh_Click(sender, e)
    + dgStudents_SelectionChanged(sender, e)
}

MainViewModel --> IStudentRepository : использует
MainViewModel --> DatabaseManager : использует
MainViewModel o-- Student : агрегация
MainWindow *-- MainViewModel : композиция
@enduml
