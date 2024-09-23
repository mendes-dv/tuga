using GestMiniMercados.Models;
using Microsoft.EntityFrameworkCore;

namespace GestMiniMercados.Data;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext ()
    {
    }

    public DatabaseContext (DbContextOptions<DatabaseContext > options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<Feria> Ferias { get; set; }

    public virtual DbSet<Fornecedor> Fornecedors { get; set; }

    public virtual DbSet<HorarioDeTrabalho> HorarioDeTrabalhos { get; set; }

    public virtual DbSet<InfoFornecedore> InfoFornecedores { get; set; }

    public virtual DbSet<InfoProd> InfoProds { get; set; }

    public virtual DbSet<IvaCategorium> IvaCategoria { get; set; }

    public virtual DbSet<ProdsPerCat> ProdsPerCats { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=GestMiniMercadosDB;User Id=sa;Password=YourStrong!Passw0rd;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__CB903349725D1094");

            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.NomeCategoria)
                .HasMaxLength(255)
                .HasColumnName("Nome_Categoria");
        });

        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => new { e.IdFornecedor, e.IdProduto }).HasName("PK__Entrega__ADD6E3B10D715E16");

            entity.ToTable("Entrega");

            entity.Property(e => e.IdFornecedor).HasColumnName("Id_Fornecedor");
            entity.Property(e => e.IdProduto).HasColumnName("Id_Produto");
            entity.Property(e => e.DataEntrega)
                .HasColumnType("datetime")
                .HasColumnName("Data_Entrega");
        });

        modelBuilder.Entity<Feria>(entity =>
        {
            entity.HasKey(e => new { e.NContribuinte, e.DataInicioFerias }).HasName("PK__Ferias__53E2BA2DA9D7D944");

            entity.Property(e => e.NContribuinte).HasColumnName("N_Contribuinte");
            entity.Property(e => e.DataInicioFerias)
                .HasColumnType("datetime")
                .HasColumnName("Data_Inicio_Ferias");
            entity.Property(e => e.DataFimFerias)
                .HasColumnType("datetime")
                .HasColumnName("Data_Fim_Ferias");
        });

        modelBuilder.Entity<Fornecedor>(entity =>
        {
            entity.HasKey(e => e.IdFornecedor).HasName("PK__Forneced__349893FCED4DCEF4");

            entity.ToTable("Fornecedor");

            entity.Property(e => e.IdFornecedor).HasColumnName("Id_Fornecedor");
            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.Morada).HasMaxLength(255);
            entity.Property(e => e.NContribuinte).HasColumnName("N_Contribuinte");
            entity.Property(e => e.NomeFornecedor)
                .HasMaxLength(255)
                .HasColumnName("Nome_Fornecedor");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Fornecedors)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fornecedo__Id_Ca__2D27B809");
        });

        modelBuilder.Entity<HorarioDeTrabalho>(entity =>
        {
            entity.HasKey(e => new { e.NContribuinte, e.DataInicioTrabalho }).HasName("PK__HorarioD__D1C42D8516A9A7D0");

            entity.ToTable("HorarioDeTrabalho");

            entity.Property(e => e.NContribuinte).HasColumnName("N_Contribuinte");
            entity.Property(e => e.DataInicioTrabalho)
                .HasColumnType("datetime")
                .HasColumnName("Data_Inicio_Trabalho");
            entity.Property(e => e.DataFimTrabalho)
                .HasColumnType("datetime")
                .HasColumnName("Data_Fim_Trabalho");
        });

        modelBuilder.Entity<InfoFornecedore>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Morada).HasMaxLength(255);
            entity.Property(e => e.NContribuinte).HasColumnName("N_Contribuinte");
            entity.Property(e => e.NomeCategoriaFornecida)
                .HasMaxLength(255)
                .HasColumnName("Nome_CategoriaFornecida");
            entity.Property(e => e.NomeFornecedor)
                .HasMaxLength(255)
                .HasColumnName("Nome_Fornecedor");
        });

        modelBuilder.Entity<InfoProd>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.MargemLucro).HasColumnName("Margem_Lucro");
            entity.Property(e => e.NomeProduto)
                .HasMaxLength(255)
                .HasColumnName("Nome_Produto");
            entity.Property(e => e.PreçoCampanha).HasColumnName("Preço_Campanha");
            entity.Property(e => e.PreçoCompra).HasColumnName("Preço_Compra");
            entity.Property(e => e.PreçoVenda).HasColumnName("Preço_Venda");
            entity.Property(e => e.QtdAtualProduto).HasColumnName("Qtd_Atual_Produto");
            entity.Property(e => e.QtdMaximaProduto).HasColumnName("Qtd_Maxima_Produto");
        });

        modelBuilder.Entity<IvaCategorium>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.IvaCategoria).HasColumnName("Iva_Categoria");
            entity.Property(e => e.NomeCategoria)
                .HasMaxLength(255)
                .HasColumnName("Nome_Categoria");
        });

        modelBuilder.Entity<ProdsPerCat>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProdsPerCat");

            entity.Property(e => e.NomeCategoria)
                .HasMaxLength(255)
                .HasColumnName("Nome_Categoria");
            entity.Property(e => e.QtdProds).HasColumnName("Qtd_Prods");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.IdProduto).HasName("PK__Produto__94E704D8F53C8DBB");

            entity.ToTable("Produto");

            entity.Property(e => e.IdProduto).HasColumnName("Id_Produto");
            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.MargemUnitaria).HasColumnName("Margem_Unitaria");
            entity.Property(e => e.NomeProduto)
                .HasMaxLength(255)
                .HasColumnName("Nome_Produto");
            entity.Property(e => e.PrecoCampanha).HasColumnName("Preco_Campanha");
            entity.Property(e => e.PrecoUnitarioCompra).HasColumnName("Preco_Unitario_Compra");
            entity.Property(e => e.PrecoUnitarioVenda).HasColumnName("Preco_Unitario_Venda");
            entity.Property(e => e.QtdProduto).HasColumnName("Qtd_Produto");
            entity.Property(e => e.QtdTotal).HasColumnName("Qtd_Total");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Produto__Id_Cate__267ABA7A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.NContribuinte).HasName("PK__Users__26035B944910114A");

            entity.Property(e => e.NContribuinte)
                .ValueGeneratedNever()
                .HasColumnName("N_Contribuinte");
            entity.Property(e => e.DataContrato)
                .HasColumnType("datetime")
                .HasColumnName("Data_Contrato");
            entity.Property(e => e.DataNascimento)
                .HasColumnType("datetime")
                .HasColumnName("Data_Nascimento");
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(50)
                .HasColumnName("Estado_Civil");
            entity.Property(e => e.Morada).HasMaxLength(255);
            entity.Property(e => e.NomeFuncionario)
                .HasMaxLength(255)
                .HasColumnName("Nome_Funcionario");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.TpUtilizador)
                .HasMaxLength(50)
                .HasColumnName("Tp_Utilizador");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.NContribuinte).HasName("PK__UserLogi__26035B9450BDEB23");

            entity.ToTable("UserLogin");

            entity.Property(e => e.NContribuinte)
                .ValueGeneratedNever()
                .HasColumnName("N_Contribuinte");
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
