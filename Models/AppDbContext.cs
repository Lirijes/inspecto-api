using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace inspecto_API.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity_log> activity_logs { get; set; }

    public virtual DbSet<Case> cases { get; set; }

    public virtual DbSet<Inspection> inspections { get; set; }

    public virtual DbSet<Facility> facilities { get; set; }

    public virtual DbSet<Profile> profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "oauth_authorization_status", new[] { "pending", "approved", "denied", "expired" })
            .HasPostgresEnum("auth", "oauth_client_type", new[] { "public", "confidential" })
            .HasPostgresEnum("auth", "oauth_registration_type", new[] { "dynamic", "manual" })
            .HasPostgresEnum("auth", "oauth_response_type", new[] { "code" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresEnum("storage", "buckettype", new[] { "STANDARD", "ANALYTICS", "VECTOR" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<Activity_log>(entity =>
        {
            entity.HasKey(e => e.id).HasName("activity_logs_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.details).HasColumnType("jsonb");
        });

        modelBuilder.Entity<Case>(entity =>
        {
            entity.ToTable("cases");

            entity.HasKey(e => e.id).HasName("cases_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.priority).HasDefaultValueSql("'medium'::text");
            entity.Property(e => e.status).HasDefaultValueSql("'open'::text");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Inspection).WithMany(p => p.Cases)
                .HasForeignKey(d => d.inspection_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("cases_inspection_id_fkey");
        });

        modelBuilder.Entity<Inspection>(entity =>
        {
            entity.ToTable("inspections"); 

            entity.HasKey(e => e.id).HasName("inspections_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.status).HasDefaultValueSql("'scheduled'::text");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Facility).WithMany(p => p.Inspections)
                .HasForeignKey(d => d.object_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("inspections_object_id_fkey");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.ToTable("objects");

            entity.HasKey(e => e.id).HasName("objects_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.status).HasDefaultValueSql("'active'::text");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.id).HasName("profiles_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
