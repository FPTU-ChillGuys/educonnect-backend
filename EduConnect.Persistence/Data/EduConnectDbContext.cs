using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EduConnect.Domain.Entities;

namespace EduConnect.Persistence.Data
{
	public class EduConnectDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
	{
		public EduConnectDbContext(DbContextOptions<EduConnectDbContext> options)
			: base(options) { }

		public DbSet<Student> Students { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<ClassSession> ClassSessions { get; set; }
		public DbSet<ClassBehaviorLog> ClassBehaviorLogs { get; set; }
		public DbSet<StudentBehaviorNote> StudentBehaviorNotes { get; set; }
		public DbSet<ClassReport> ClassReports { get; set; }
		public DbSet<StudentReport> StudentReports { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Message> Messages { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Class
			modelBuilder.Entity<Class>()
				.HasOne(c => c.HomeroomTeacher)
				.WithMany(u => u.HomeroomClasses)
				.HasForeignKey(c => c.HomeroomTeacherId)
				.OnDelete(DeleteBehavior.Restrict);

			// Student
			modelBuilder.Entity<Student>()
				.HasOne(s => s.Class)
				.WithMany(c => c.Students)
				.HasForeignKey(s => s.ClassId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Student>()
				.HasOne(s => s.Parent)
				.WithMany(u => u.Children)
				.HasForeignKey(s => s.ParentId)
				.OnDelete(DeleteBehavior.Restrict);

			// Class Period
			modelBuilder.Entity<ClassSession>()
				.HasOne(cs => cs.Subject)
				.WithMany(s => s.ClassSessions)
				.HasForeignKey(cs => cs.SubjectId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ClassSession>()
				.HasOne(cs => cs.Teacher)
				.WithMany(u => u.TeachingSessions)
				.HasForeignKey(cs => cs.TeacherId)
				.OnDelete(DeleteBehavior.Restrict);

			// ClassBehaviorLog 
			modelBuilder.Entity<ClassBehaviorLog>()
				.HasOne(l => l.ClassSession)
				.WithMany(n => n.ClassBehaviorLogs)
				.HasForeignKey(l => l.ClassSessionId)
				.OnDelete(DeleteBehavior.Cascade);

			// StudentBehaviorNote 
			modelBuilder.Entity<StudentBehaviorNote>()
				.HasOne(note => note.ClassSession)
				.WithMany(cs => cs.StudentBehaviorNotes)
				.HasForeignKey(note => note.ClassSessionId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<StudentBehaviorNote>()
				.HasOne(note => note.Student)
				.WithMany(s => s.BehaviorNotes)
				.HasForeignKey(note => note.StudentId)
				.OnDelete(DeleteBehavior.Restrict);

			// Class Reports 
			modelBuilder.Entity<ClassReport>()
				.HasOne(r => r.Class)
				.WithMany(c => c.ClassReports)
				.HasForeignKey(r => r.ClassId)
				.OnDelete(DeleteBehavior.Restrict);

			// Student Reports
			modelBuilder.Entity<StudentReport>()
				.HasOne(r => r.Student)
				.WithMany(s => s.StudentReports)
				.HasForeignKey(r => r.StudentId)
				.OnDelete(DeleteBehavior.Restrict);

            //Conversation
			modelBuilder.Entity<Conversation>()
				.HasOne(c => c.Parent)
				.WithMany(u => u.Conversations)
				.HasForeignKey(c => c.ParentId)
				.OnDelete(DeleteBehavior.Restrict);

            // Message
            modelBuilder.Entity<Message>()
				.HasOne(m => m.Conversation)
				.WithMany(u => u.Messages)
				.HasForeignKey(m => m.ConversationId)
				.OnDelete(DeleteBehavior.Restrict);

			// Notification
			modelBuilder.Entity<Notification>()
				.HasOne(n => n.ClassReport)
				.WithMany(r => r.Notifications)
				.HasForeignKey(n => n.ClassReportId)
				.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Notification>()
				.HasOne(n => n.StudentReport)
				.WithMany(r => r.Notifications)
				.HasForeignKey(n => n.StudentReportId)
				.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Notification>()
				.HasOne(n => n.Recipient)
				.WithMany(u => u.Notifications)
				.HasForeignKey(n => n.RecipientUserId)
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
