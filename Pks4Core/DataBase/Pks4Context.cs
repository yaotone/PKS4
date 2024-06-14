using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pks4Core;
public partial class Pks4Context : DbContext
{
   
    public Pks4Context(DbContextOptions<Pks4Context> options) : base(options)
    {
    }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("messages_pkey");

            entity.ToTable("messages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.MessageText).HasColumnName("message_text");
            entity.Property(e => e.MessageTitle).HasColumnName("message_title");
            entity.Property(e => e.SendingTime).HasColumnName("sending_time");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.To).HasColumnName("to");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.SecondName).HasColumnName("second_name");
            entity.Property(e => e.ThirdName).HasColumnName("third_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
