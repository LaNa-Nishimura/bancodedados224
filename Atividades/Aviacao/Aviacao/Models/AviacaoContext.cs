using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Aviacao.Models;

public partial class AviacaoContext : DbContext
{
    public AviacaoContext()
    {
    }

    public AviacaoContext(DbContextOptions<AviacaoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aeronave> Aeronaves { get; set; }

    public virtual DbSet<Aeroporto> Aeroportos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Escala> Escalas { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<TipoAeronave> TipoAeronaves { get; set; }

    public virtual DbSet<Voo> Voos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AVIACAO;User ID=;Password=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aeronave>(entity =>
        {
            entity.HasKey(e => e.IdAeronave).HasName("PK__AERONAVE__8D0DAF56618A5018");

            entity.ToTable("AERONAVE");

            entity.Property(e => e.IdAeronave)
                .ValueGeneratedNever()
                .HasColumnName("ID_AERONAVE");
            entity.Property(e => e.IdTipoAeronave).HasColumnName("ID_TIPO_AERONAVE");
            entity.Property(e => e.QtdPoltronas).HasColumnName("QTD_POLTRONAS");

            entity.HasOne(d => d.IdTipoAeronaveNavigation).WithMany(p => p.Aeronaves)
                .HasForeignKey(d => d.IdTipoAeronave)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_TIPO_AERONAVE");
        });

        modelBuilder.Entity<Aeroporto>(entity =>
        {
            entity.HasKey(e => e.IdAeroporto).HasName("PK__AEROPORT__21947E402C721F7D");

            entity.ToTable("AEROPORTO");

            entity.Property(e => e.IdAeroporto)
                .ValueGeneratedNever()
                .HasColumnName("ID_AEROPORTO");
            entity.Property(e => e.Localizacao)
                .HasColumnType("text")
                .HasColumnName("LOCALIZACAO");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__CLIENTE__23A341302619BF04");

            entity.ToTable("CLIENTE");

            entity.Property(e => e.IdCliente)
                .ValueGeneratedNever()
                .HasColumnName("ID_CLIENTE");
            entity.Property(e => e.Contato)
                .HasColumnType("text")
                .HasColumnName("CONTATO");
            entity.Property(e => e.DataNascimento)
                .HasColumnType("datetime")
                .HasColumnName("DATA_NASCIMENTO");
            entity.Property(e => e.Genero)
                .HasColumnType("text")
                .HasColumnName("GENERO");
            entity.Property(e => e.Nome)
                .HasColumnType("text")
                .HasColumnName("NOME");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK_dbo.COMPRA");

            entity.ToTable("COMPRA");

            entity.Property(e => e.IdCompra).HasColumnName("ID_COMPRA");
            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");
            entity.Property(e => e.IdHorario).HasColumnName("ID_HORARIO");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_dbo.COMPRA_dbo.CLIENTE_IDCLIENTE");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdHorario)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_dbo.COMPRA_dbo.HORARIO_IDHORARIO");
        });

        modelBuilder.Entity<Escala>(entity =>
        {
            entity.HasKey(e => e.IdEscala).HasName("PK__ESCALA__C83F0846C3E08799");

            entity.ToTable("ESCALA");

            entity.Property(e => e.IdEscala)
                .ValueGeneratedNever()
                .HasColumnName("ID_ESCALA");
            entity.Property(e => e.HorarioSaida)
                .HasColumnType("datetime")
                .HasColumnName("HORARIO_SAIDA");
            entity.Property(e => e.IdAeroportoSaida).HasColumnName("ID_AEROPORTO_SAIDA");
            entity.Property(e => e.IdVoo).HasColumnName("ID_VOO");

            entity.HasOne(d => d.IdAeroportoSaidaNavigation).WithMany(p => p.Escalas)
                .HasForeignKey(d => d.IdAeroportoSaida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_AEROPORTO_SAIDA_ESCALA");

            entity.HasOne(d => d.IdVooNavigation).WithMany(p => p.Escalas)
                .HasForeignKey(d => d.IdVoo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_VOO_ESCALA");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK_dbo.HORARIO");

            entity.ToTable("HORARIO");

            entity.Property(e => e.IdHorario).HasColumnName("ID_HORARIO");
            entity.Property(e => e.Disponibilidade)
                .HasDefaultValue(1)
                .HasColumnName("DISPONIBILIDADE");
            entity.Property(e => e.IdVoo).HasColumnName("ID_VOO");
            entity.Property(e => e.LadoPoltrona)
                .HasColumnType("ntext")
                .HasColumnName("LADO_POLTRONA");
            entity.Property(e => e.LocalizacaoPoltrona)
                .HasColumnType("ntext")
                .HasColumnName("LOCALIZACAO_POLTRONA");

            entity.HasOne(d => d.IdVooNavigation).WithMany(p => p.Horarios)
                .HasForeignKey(d => d.IdVoo)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_dbo.HORARIO_dbo.VOO_IDVOO");
        });

        modelBuilder.Entity<TipoAeronave>(entity =>
        {
            entity.HasKey(e => e.IdTipoAeronave).HasName("PK__TIPO_AER__329537620C32AB3E");

            entity.ToTable("TIPO_AERONAVE");

            entity.Property(e => e.IdTipoAeronave)
                .ValueGeneratedNever()
                .HasColumnName("ID_TIPO_AERONAVE");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("DESCRICAO");
        });

        modelBuilder.Entity<Voo>(entity =>
        {
            entity.HasKey(e => e.IdVoo).HasName("PK__VOO__273B8C65F30C0065");

            entity.ToTable("VOO");

            entity.Property(e => e.IdVoo)
                .ValueGeneratedNever()
                .HasColumnName("ID_VOO");
            entity.Property(e => e.HorarioDestino)
                .HasColumnType("datetime")
                .HasColumnName("HORARIO_DESTINO");
            entity.Property(e => e.HorarioSaida)
                .HasColumnType("datetime")
                .HasColumnName("HORARIO_SAIDA");
            entity.Property(e => e.IdAeronave).HasColumnName("ID_AERONAVE");
            entity.Property(e => e.IdAeroportoDestino).HasColumnName("ID_AEROPORTO_DESTINO");
            entity.Property(e => e.IdAeroportoSaida).HasColumnName("ID_AEROPORTO_SAIDA");

            entity.HasOne(d => d.IdAeronaveNavigation).WithMany(p => p.Voos)
                .HasForeignKey(d => d.IdAeronave)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_AERONAVE_VOO");

            entity.HasOne(d => d.IdAeroportoDestinoNavigation).WithMany(p => p.VooIdAeroportoDestinoNavigations)
                .HasForeignKey(d => d.IdAeroportoDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_AEROPORTO_DESTINO");

            entity.HasOne(d => d.IdAeroportoSaidaNavigation).WithMany(p => p.VooIdAeroportoSaidaNavigations)
                .HasForeignKey(d => d.IdAeroportoSaida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ID_AEROPORTO_SAIDA_VOO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
