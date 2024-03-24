using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PsycoTest.Models
{
    public partial class PsycoTestDataContext : DbContext
    {
        public PsycoTestDataContext()
        {
        }

        public PsycoTestDataContext(DbContextOptions<PsycoTestDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }

        public virtual DbSet<AnswerResult> AnswerResults { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<Question> Questions { get; set; }

        public virtual DbSet<Result> Results { get; set; }

        public virtual DbSet<Test> Tests { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=LAPTOP-MGF1A46M\\SQLEXPRESS;Database=DBPsycoTest;Trusted_Connection=True;Encrypt=False;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.AnswerId).ValueGeneratedNever();
                entity.Property(e => e.Answer1).HasMaxLength(100);
                entity.Property(e => e.Answer10).HasMaxLength(100);
                entity.Property(e => e.Answer2).HasMaxLength(100);
                entity.Property(e => e.Answer3).HasMaxLength(100);
                entity.Property(e => e.Answer4).HasMaxLength(100);
                entity.Property(e => e.Answer5).HasMaxLength(100);
                entity.Property(e => e.Answer6).HasMaxLength(100);
                entity.Property(e => e.Answer7).HasMaxLength(100);
                entity.Property(e => e.Answer8).HasMaxLength(100);
                entity.Property(e => e.Answer9).HasMaxLength(100);
                entity.Property(e => e.AnswerQuestionId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.AnswerQuestion).WithMany(p => p.Answers)
                    .HasForeignKey(d => d.AnswerQuestionId)
                    .HasConstraintName("FK_Answer_Question");
            });

            modelBuilder.Entity<AnswerResult>(entity =>
            {
                entity.HasKey(e => e.ResultId);

                entity.ToTable("AnswerResult");

                entity.Property(e => e.ResultId).ValueGeneratedNever();

                entity.HasOne(d => d.Answer).WithMany(p => p.AnswerResults)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK_AnswerResult_Answer");

                entity.HasOne(d => d.Result).WithOne(p => p.AnswerResult)
                    .HasForeignKey<AnswerResult>(d => d.ResultId)
                    .HasConstraintName("FK_AnswerResult_Result");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.GroupNumber).HasMaxLength(3);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.QuestionName).HasMaxLength(180);

                entity.HasOne(d => d.QuestionTest).WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionTestId)
                    .HasConstraintName("FK_Question_Test");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.ToTable("Result");

                entity.Property(e => e.ResultId).ValueGeneratedNever();
                entity.Property(e => e.ResultDateEnd).HasColumnType("datetime");
                entity.Property(e => e.ResultDateStart).HasColumnType("datetime");
                entity.Property(e => e.ResultTestId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.ResultTest).WithMany(p => p.Results)
                    .HasForeignKey(d => d.ResultTestId)
                    .HasConstraintName("FK_Result_Test");

                entity.HasOne(d => d.ResultUser).WithMany(p => p.Results)
                    .HasForeignKey(d => d.ResultUserId)
                    .HasConstraintName("FK_Result_User");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("Test");

                entity.Property(e => e.TestName).HasMaxLength(150);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserFirstName).HasMaxLength(100);
                entity.Property(e => e.UserLastName).HasMaxLength(120);
                entity.Property(e => e.UserLogin).HasMaxLength(70);
                entity.Property(e => e.UserPassword).HasMaxLength(20);
                entity.Property(e => e.UserPatronymic).HasMaxLength(90);

                entity.HasOne(d => d.UserGroup).WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserGroupId)
                    .HasConstraintName("FK_User_Group");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}