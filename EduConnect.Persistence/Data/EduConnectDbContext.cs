using EduConnect.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Persistence.Data
{
	public class EduConnectDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
	{
		public EduConnectDbContext(DbContextOptions<EduConnectDbContext> options)
			: base(options) { }

		public DbSet<Classroom> Classrooms { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<ClassNotebook> ClassNotebooks { get; set; }
		public DbSet<LessonLog> LessonLogs { get; set; }
		public DbSet<StudentLessonNote> StudentLessonNotes { get; set; }
		public DbSet<Reminder> Reminders { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Notification> Notifications { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Classroom
			modelBuilder.Entity<Classroom>()
				.HasOne(c => c.HomeroomTeacher)
				.WithMany()
				.HasForeignKey(c => c.HomeroomTeacherID)
				.OnDelete(DeleteBehavior.Restrict);

			// Student
			modelBuilder.Entity<Student>()
				.HasOne(s => s.Class)
				.WithMany(c => c.Students)
				.HasForeignKey(s => s.ClassID);

			modelBuilder.Entity<Student>()
				.HasOne(s => s.Parent)
				.WithMany(u => u.Students)
				.HasForeignKey(s => s.ParentID)
				.OnDelete(DeleteBehavior.Restrict);

			// Schedule
			modelBuilder.Entity<Schedule>()
				.HasOne(s => s.Class)
				.WithMany(c => c.Schedules)
				.HasForeignKey(s => s.ClassID);

			modelBuilder.Entity<Schedule>()
				.HasOne(s => s.Subject)
				.WithMany()
				.HasForeignKey(s => s.SubjectID);

			modelBuilder.Entity<Schedule>()
				.HasOne(s => s.Teacher)
				.WithMany(u => u.Schedules)
				.HasForeignKey(s => s.TeacherID)
				.OnDelete(DeleteBehavior.Restrict);

			// ClassNotebook
			modelBuilder.Entity<ClassNotebook>()
				.HasOne(n => n.Class)
				.WithMany(c => c.ClassNotebooks)
				.HasForeignKey(n => n.ClassID);

			modelBuilder.Entity<ClassNotebook>()
				.HasOne(n => n.Creator)
				.WithMany(u => u.CreatedNotebooks)
				.HasForeignKey(n => n.CreatedBy)
				.OnDelete(DeleteBehavior.Restrict);

			// LessonLog
			modelBuilder.Entity<LessonLog>()
				.HasOne(l => l.Notebook)
				.WithMany(n => n.LessonLogs)
				.HasForeignKey(l => l.NotebookID);

			modelBuilder.Entity<LessonLog>()
				.HasOne(l => l.Schedule)
				.WithMany()
				.HasForeignKey(l => l.ScheduleID)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<LessonLog>()
				.HasOne(l => l.Teacher)
				.WithMany(u => u.LessonLogs)
				.HasForeignKey(l => l.TeacherID)
				.OnDelete(DeleteBehavior.Restrict);

			// StudentLessonNote
			modelBuilder.Entity<StudentLessonNote>()
				.HasOne(sn => sn.LessonLog)
				.WithMany(l => l.StudentLessonNotes)
				.HasForeignKey(sn => sn.LessonLogID);

			modelBuilder.Entity<StudentLessonNote>()
				.HasOne(sn => sn.Student)
				.WithMany(s => s.LessonNotes)
				.HasForeignKey(sn => sn.StudentID)
				.OnDelete(DeleteBehavior.NoAction);

			// Reminder
			modelBuilder.Entity<Reminder>()
				.HasOne(r => r.Class)
				.WithMany(c => c.Reminders)
				.HasForeignKey(r => r.ClassID);

			modelBuilder.Entity<Reminder>()
				.HasOne(r => r.Creator)
				.WithMany(u => u.Reminders)
				.HasForeignKey(r => r.CreatedBy)
				.OnDelete(DeleteBehavior.Restrict);

			// Message
			modelBuilder.Entity<Message>()
				.HasOne(m => m.FromUser)
				.WithMany(u => u.SentMessages)
				.HasForeignKey(m => m.FromUserID)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Message>()
				.HasOne(m => m.ToUser)
				.WithMany(u => u.ReceivedMessages)
				.HasForeignKey(m => m.ToUserID)
				.OnDelete(DeleteBehavior.Restrict);

			// Notification
			modelBuilder.Entity<Notification>()
				.HasOne(n => n.Student)
				.WithMany(s => s.Notifications)
				.HasForeignKey(n => n.StudentID);

			modelBuilder.Entity<Notification>()
				.HasOne(n => n.Sender)
				.WithMany(u => u.Notifications)
				.HasForeignKey(n => n.SentBy)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<IdentityRole<Guid>>().HasData(SeedingRoles());
			modelBuilder.Entity<User>().HasData(SeedingUsers());
			modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(SeedingUserRoles());
		}

		private ICollection<IdentityRole<Guid>> SeedingRoles()
		{
			return new List<IdentityRole<Guid>>()
			{
				new IdentityRole<Guid>
				{
					Id = Guid.Parse("3631e38b-60dd-4d1a-af7f-a26f21c2ef82"),
					Name = "admin",
					NormalizedName = "ADMIN",
					ConcurrencyStamp = "seed-1"
				},
				new IdentityRole<Guid>
				{
					Id = Guid.Parse("51ef7e08-ff07-459b-8c55-c7ebac505103"),
					Name = "teacher",
					NormalizedName = "TEACHER",
					ConcurrencyStamp = "seed-2"
				},
				new IdentityRole<Guid>
				{
					Id = Guid.Parse("37a7c5df-4898-4fd4-8e5f-d2abd4b57520"),
					Name = "parent",
					NormalizedName = "PARENT",
					ConcurrencyStamp = "seed-3"
				}
			};
		}

		private ICollection<User> SeedingUsers()
		{
			var hasher = new PasswordHasher<User>();

			var admin = new User
			{
				Id = Guid.Parse("33f41895-b601-4aa1-8dc4-8229a9d07008"),
				UserName = "admin",
				NormalizedUserName = "ADMIN",
				Email = "admin@example.com",
				NormalizedEmail = "ADMIN@EXAMPLE.COM",
				EmailConfirmed = true,
				SecurityStamp = "seed-4",
				ConcurrencyStamp = "seed-5",
				PasswordHash = hasher.HashPassword(null!, "12345aA@")
			};

			var teacher = new User
			{
				Id = Guid.Parse("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
				UserName = "teacher",
				NormalizedUserName = "TEACHER",
				Email = "teacher@example.com",
				NormalizedEmail = "TEACHER@EXAMPLE.COM",
				EmailConfirmed = true,
				SecurityStamp = "seed-6",
				ConcurrencyStamp = "seed-7",
				PasswordHash = hasher.HashPassword(null!, "12345aA@")
			};

			var parent = new User
			{
				Id = Guid.Parse("fe014130-bfb5-443b-9989-9c8f90d1065f"),
				UserName = "parent",
				NormalizedUserName = "PARENT",
				Email = "parent@example.com",
				NormalizedEmail = "PARENT@EXAMPLE.COM",
				EmailConfirmed = true,
				SecurityStamp = "seed-8",
				ConcurrencyStamp = "seed-9",
				PasswordHash = hasher.HashPassword(null!, "12345aA@")
			};

			return new List<User> { admin, teacher, parent };
		}

		private ICollection<IdentityUserRole<Guid>> SeedingUserRoles()
		{
			return new List<IdentityUserRole<Guid>>
			{
				new IdentityUserRole<Guid>
				{
					UserId = Guid.Parse("33f41895-b601-4aa1-8dc4-8229a9d07008"),
					RoleId = Guid.Parse("3631e38b-60dd-4d1a-af7f-a26f21c2ef82")
				},
				new IdentityUserRole<Guid>
				{
					UserId = Guid.Parse("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
					RoleId = Guid.Parse("51ef7e08-ff07-459b-8c55-c7ebac505103")
				},
				new IdentityUserRole<Guid>
				{
					UserId = Guid.Parse("fe014130-bfb5-443b-9989-9c8f90d1065f"),
					RoleId = Guid.Parse("37a7c5df-4898-4fd4-8e5f-d2abd4b57520")
				}
			};
		}
	}
}
