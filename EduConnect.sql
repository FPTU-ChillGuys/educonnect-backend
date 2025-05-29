-- ============================================
-- CREATE DATABASE
-- ============================================
CREATE DATABASE EduConnectDB;
GO

USE EduConnectDB;
GO

-- ============================================
-- TABLE: Roles
-- ============================================
--CREATE TABLE Roles (
--    role_id INT IDENTITY(1,1) PRIMARY KEY,
--    role_name NVARCHAR(50)
--);

--INSERT INTO Roles (role_name) VALUES
--(N'Teacher'), (N'Parent'), (N'Administrator');

-- ============================================
-- TABLE: Users
-- ============================================
CREATE TABLE Users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255),
    email NVARCHAR(255),
    phone_number NVARCHAR(50),
    password NVARCHAR(255),
    role INT FOREIGN KEY REFERENCES Roles(role_id)
);

-- Insert users
INSERT INTO Users (name, email, phone_number, password, role) VALUES
(N'Nguyễn Văn A', N'a@gmail.com', '0911222333', 'matkhau123', 1), -- Teacher
(N'Trần Thị B', N'b@gmail.com', '0988777666', '12345678', 2),
(N'Nguyễn Văn B', N'ph_b1@gmail.com', '0911000001', 'pw123', 2),
(N'Lê Thị C', N'ph_c2@gmail.com', '0911000002', 'pw123', 2),
(N'Hoàng Văn D', N'ph_d3@gmail.com', '0911000003', 'pw123', 2),
(N'Trần Thị E', N'ph_e4@gmail.com', '0911000004', 'pw123', 2),
(N'Bùi Văn F', N'ph_f5@gmail.com', '0911000005', 'pw123', 2),
(N'Lê Văn C', N'admin@edu.vn', '0909123123', 'adminpass', 3); -- Admin

-- ============================================
-- TABLE: Teachers
-- ============================================
CREATE TABLE Teachers (
    teacher_id INT IDENTITY(101,1) PRIMARY KEY,
    user_id INT UNIQUE FOREIGN KEY REFERENCES Users(user_id)
);

-- 1 teacher: user_id = 1
INSERT INTO Teachers (user_id) VALUES (1);

-- ============================================
-- TABLE: Parents
-- ============================================
CREATE TABLE Parents (
    parent_id INT IDENTITY(201,1) PRIMARY KEY,
    user_id INT UNIQUE FOREIGN KEY REFERENCES Users(user_id)
);

-- 6 parents: user_id = 2 to 7
INSERT INTO Parents (user_id) VALUES (2), (3), (4), (5), (6), (7);

-- ============================================
-- TABLE: Classes
-- ============================================
CREATE TABLE Classes (
    class_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255),
    HomeroomTeacherId INT FOREIGN KEY REFERENCES Teachers(teacher_id)
);

-- 1 class, teacher_id = 101
INSERT INTO Classes (name, HomeroomTeacherId) VALUES (N'Lớp 10A1', 101);

-- ============================================
-- TABLE: Students
-- ============================================
CREATE TABLE Students (
    student_id INT IDENTITY(301,1) PRIMARY KEY,
    class_id INT FOREIGN KEY REFERENCES Classes(class_id),
    name NVARCHAR(255)
);

INSERT INTO Students (class_id, name) VALUES 
(1, N'Phạm Minh Đức'),
(1, N'Nguyễn Thị Hạnh'),
(1, N'Đỗ Văn Minh'),
(1, N'Lê Thị Lan'),
(1, N'Phan Văn Bình'),
(1, N'Trịnh Thị Hoa');

-- ============================================
-- TABLE: ParentStudent
-- ============================================
CREATE TABLE ParentStudent (
    parent_id INT,
    student_id INT,
    PRIMARY KEY (parent_id, student_id),
    FOREIGN KEY (parent_id) REFERENCES Parents(parent_id),
    FOREIGN KEY (student_id) REFERENCES Students(student_id)
);

INSERT INTO ParentStudent VALUES 
(201, 301),
(202, 302),
(203, 303),
(204, 304),
(205, 305),
(206, 306);

-- ============================================
-- TABLE: Subjects
-- ============================================
CREATE TABLE Subjects (
    subject_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255)
);

INSERT INTO Subjects (name) VALUES 
(N'Mathematics'),
(N'Literature');

-- ============================================
-- TABLE: Timetables
-- ============================================
CREATE TABLE Timetables (
    timetable_id INT IDENTITY(401,1) PRIMARY KEY,
    class_id INT FOREIGN KEY REFERENCES Classes(class_id)
);

INSERT INTO Timetables (class_id) VALUES (1);

-- ============================================
-- TABLE: Periods
-- ============================================
CREATE TABLE Periods (
    period_id INT IDENTITY(501,1) PRIMARY KEY,
    timetable_id INT FOREIGN KEY REFERENCES Timetables(timetable_id),
    teacher_id INT FOREIGN KEY REFERENCES Teachers(teacher_id),
    subject_id INT FOREIGN KEY REFERENCES Subjects(subject_id),
    date DATE,
    lesson_index INT
);

INSERT INTO Periods (timetable_id, teacher_id, subject_id, date, lesson_index) VALUES 
(401, 101, 1, '2025-05-26', 1);

-- ============================================
-- TABLE: Logbooks
-- ============================================
CREATE TABLE Logbooks (
    logbook_id INT IDENTITY(601,1) PRIMARY KEY,
    class_id INT FOREIGN KEY REFERENCES Classes(class_id)
);

INSERT INTO Logbooks (class_id) VALUES (1);

-- ============================================
-- TABLE: LogbookDetails
-- ============================================
CREATE TABLE LogbookDetails (
    logbook_id INT,
    class_id INT,
    period_id INT,
    note NVARCHAR(MAX),
    PRIMARY KEY (logbook_id, period_id),
    FOREIGN KEY (logbook_id) REFERENCES Logbooks(logbook_id),
    FOREIGN KEY (class_id) REFERENCES Classes(class_id),
    FOREIGN KEY (period_id) REFERENCES Periods(period_id)
);

INSERT INTO LogbookDetails (logbook_id, class_id, period_id, note) VALUES 
(601, 1, 501, N'Học sinh tích cực phát biểu, có tiến bộ rõ rệt.');

-- ============================================
-- TABLE: StudentNote
-- ============================================
CREATE TABLE StudentNote (
    studentnote_id INT IDENTITY(701,1) PRIMARY KEY,
    student_id INT FOREIGN KEY REFERENCES Students(student_id),
    logbook_id INT FOREIGN KEY REFERENCES Logbooks(logbook_id),
    content NVARCHAR(MAX)
);

INSERT INTO StudentNote (student_id, logbook_id, content) VALUES 
(301, 601, N'Cần cải thiện kỹ năng giải toán.');

-- ============================================
-- TABLE: Reminders
-- ============================================
CREATE TABLE Reminders (
    reminder_id INT IDENTITY(801,1) PRIMARY KEY,
    teacher_id INT FOREIGN KEY REFERENCES Teachers(teacher_id),
    class_id INT FOREIGN KEY REFERENCES Classes(class_id),
    date DATE,
    content NVARCHAR(MAX),
    created_at DATETIME
);

INSERT INTO Reminders (teacher_id, class_id, date, content, created_at) VALUES 
(101, 1, '2025-05-28', N'Nộp bài kiểm tra giữa kỳ', GETDATE());

-- ============================================
-- TABLE: Notifications
-- ============================================
CREATE TABLE Notifications (
    notification_id INT IDENTITY(901,1) PRIMARY KEY,
    sender_id INT FOREIGN KEY REFERENCES Users(user_id),
    receiver_id INT FOREIGN KEY REFERENCES Users(user_id),
    summary_text NVARCHAR(MAX),
    frequency NVARCHAR(50),
    created_at DATETIME
);

INSERT INTO Notifications (sender_id, receiver_id, summary_text, frequency, created_at) VALUES 
(1, 2, N'Con bạn cần chú ý hơn trong môn Toán.', N'Weekly', GETDATE());

-- ============================================
-- TABLE: Messages
-- ============================================
CREATE TABLE Messages (
    message_id INT IDENTITY(1001,1) PRIMARY KEY,
    sender_id INT FOREIGN KEY REFERENCES Users(user_id),
    receiver_id INT FOREIGN KEY REFERENCES Users(user_id),
    content NVARCHAR(MAX),
    timestamp DATETIME,
    handled_by_ai BIT
);

INSERT INTO Messages (sender_id, receiver_id, content, timestamp, handled_by_ai) VALUES 
(2, 1, N'Tôi muốn biết thêm về tình hình học tập của con.', GETDATE(), 0);

-- ============================================
-- TABLE: RefeshToken
-- ============================================
CREATE TABLE RefreshToken (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    UserId NVARCHAR(450) NOT NULL,
    ExpirationTime DATETIME2 NOT NULL
);