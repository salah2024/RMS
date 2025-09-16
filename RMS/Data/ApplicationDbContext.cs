using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Controllers.AmalyateKhaki.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;
using RMS.Models.StoredProceduresData;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<clsAbadDivarBali> AbadDivarBalis { get; set; }
    public DbSet<clsAbadeKoole> AbadeKooles { get; set; }
    public DbSet<clsAbroDaliDetails> AbroDaliDetailses { get; set; }
    public DbSet<clsAmalyateKhakiInfoForBarAvord> AmalyateKhakiInfoForBarAvords { get; set; }
    public DbSet<clsAmalyateKhakiInfoForBarAvordDetails> AmalyateKhakiInfoForBarAvordDetailses { get; set; }
    public DbSet<clsAmalyateKhakiInfoForBarAvordEzafeBaha> AmalyateKhakiInfoForBarAvordEzafeBahas { get; set; }
    public DbSet<clsAmalyateKhakiInfoForBarAvordDetailsMore> AmalyateKhakiInfoForBarAvordDetailsMores { get; set; }
    public DbSet<clsAmalyateKhakiInfoForBarAvordMore> AmalyateKhakiInfoForBarAvordMores { get; set; }
    public DbSet<clsAnalizBaha> AnalizBahas { get; set; }
    public DbSet<clsAppOperationInfoDetails> AppOperationInfoDetailses { get; set; }
    public DbSet<clsAppOperationInfoMain> AppOperationInfoMains { get; set; }
    public DbSet<clsAppQuestion> AppQuestions { get; set; }
    public DbSet<clsBaravordUser> BaravordUsers { get; set; }
    public DbSet<clsBaseInfo> BaseInfos { get; set; }
    public DbSet<clsBaseInfoType> BaseInfoTypes { get; set; }
    public DbSet<clsConditionContext> ConditionContexts { get; set; }
    public DbSet<clsConditionGroup> ConditionGroups { get; set; }
    public DbSet<clsDastakPolInfo> DastakPolInfos { get; set; }
    public DbSet<clsFB> FBs { get; set; }
    public DbSet<clsFehrestBaha> FehrestBahas { get; set; }
    public DbSet<clsGeneralProjectTiming> GeneralProjectTimings { get; set; }
    public DbSet<clsHadAksarErtefaKoole> HadAksarErtefaKooles { get; set; }
    public DbSet<clsItemsAddingToFB> ItemsAddingToFBs { get; set; }
    public DbSet<clsItemsFBDependQuestionForAbnieFani> ItemsFBDependQuestionForAbnieFanis { get; set; }
    public DbSet<clsItemsFBShomarehValueShomareh> ItemsFBShomarehValueShomarehs { get; set; }
    public DbSet<clsItemsFields> ItemsFieldses { get; set; }
    public DbSet<clsItemsForGetValues> ItemsForGetValuess { get; set; }
    public DbSet<clsItemsHasCondition> ItemsHasConditions { get; set; }
    public DbSet<clsItemsHasCondition_ConditionContext> ItemsHasCondition_ConditionContexts { get; set; }
    public DbSet<clsItemsHasConditionAddedToFB> ItemsHasConditionAddedToFBs { get; set; }
    public DbSet<clsItemsHasHaml> ItemsHasHamls { get; set; }
    public DbSet<clsKiloMetrazhOfHaml> KiloMetrazhOfHamls { get; set; }
    public DbSet<clsMashinType> MashinTypes { get; set; }
    public DbSet<clsNiroMasalehMashin> NiroMasalehMashins { get; set; }
    public DbSet<clsNiroZaribKarKard> NiroZaribKarKards { get; set; }
    public DbSet<clsOperation> Operations { get; set; }
    public DbSet<clsOperation_ItemsFB> Operation_ItemsFBs { get; set; }
    public DbSet<clsOperationAmelMoaser> OperationAmelMoasers { get; set; }
    public DbSet<clsOperationHasAddedOperations> OperationHasAddedOperationses { get; set; }
    public DbSet<clsOperationHasAddedOperationsType> OperationHasAddedOperationsTypes { get; set; }
    public DbSet<clsOperationsOfHaml> OperationsOfHamls { get; set; }
    public DbSet<clsOperationsOfHaml_ItemsHasHaml> OperationsOfHaml_ItemsHasHamls { get; set; }
    public DbSet<clsOperationsOfHamlAndItems> OperationsOfHamlAndItemses { get; set; }
    public DbSet<clsPolVaAbroBarAvord> PolVaAbroBarAvords { get; set; }
    public DbSet<clsProject> Projects { get; set; }
    public DbSet<clsProjectCalendar> ProjectCalendars { get; set; }
    public DbSet<clsQuesForAbnieFani> QuesForAbnieFanis { get; set; }
    public DbSet<clsQuesForAbnieFaniValues> QuesForAbnieFaniValuess { get; set; }
    public DbSet<clsRizKiloMetrazhOfHaml> RizKiloMetrazhOfHamls { get; set; }
    public DbSet<clsRizMetreUsers> RizMetreUserses { get; set; }
    public DbSet<clsSegmentsFromGEODB> SegmentsFromGEODBs { get; set; }
    public DbSet<clsShomarehFBForQuesForAbnieFani> ShomarehFBForQuesForAbnieFanis { get; set; }
    public DbSet<clsSubItemsAddingToFB> SubItemsAddingToFBs { get; set; }
    public DbSet<clsZarayebTabdil> ZarayebTabdils { get; set; }
    public DbSet<clsZaribRoadType> ZaribRoadTypes { get; set; }
    public DbSet<clsNoeFosoul> NoeFosouls { get; set; }
    public DbSet<clsFosoul> Fosouls { get; set; }
    public DbSet<clsOperationHasAddedOperationsLevelNumber> OperationHasAddedOperationsLevelNumbers { get; set; }
    public DbSet<clsBarAvordAddedBoard> BarAvordAddedBoards { get; set; }
    public DbSet<clsBoardInfo> BoardInfos { get; set; }
    public DbSet<clsBoardItems> BoardItemses { get; set; }
    public DbSet<clsRizMetreForBarAvordAddedBoard> RizMetreForBarAvordAddedBoards { get; set; }
    public DbSet<clsBoardStandItems> BoardStandItemses { get; set; }
    public DbSet<clsBarAvordAddedBoardStand> BarAvordAddedBoardStands { get; set; }
    public DbSet<clsRizMetreForBarAvordAddedBoardStand> RizMetreForBarAvordAddedBoardStands { get; set; }
    public DbSet<clsNoeKhakBardari> NoeKhakBardaris { get; set; }
    public DbSet<clsAmalyateKhakiInfoForBarAvordDetailsRizMetre> AmalyateKhakiInfoForBarAvordDetailsRizMetres { get; set; }
    public DbSet<clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre> AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres { get; set; }
    public DbSet<clsNoeKhakBardariEzafeBaha> NoeKhakBardariEzafeBahas { get; set; }
    public DbSet<clsNoeKhakBardari_NoeKhakBardariEzafeBaha> NoeKhakBardari_NoeKhakBardariEzafeBahas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<clsOperation>()
        .HasIndex(o => new { o.Id, o.Year })
        .IsUnique(); // ترکیب Id + Year باید یکتا باشه

        modelBuilder.Entity<clsOperation>()
            .HasOne(o => o.Parent)
            .WithMany(p => p.Children)
            .HasForeignKey(o => o.ParentId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<QuesForAbnieFaniValuesDto>().HasNoKey();

        modelBuilder.Entity<ItemsFBShomarehValueShomarehUpdateProcedureDto>().HasNoKey();
        modelBuilder.Entity<ItemsFBDependQuestionForAbnieFaniForSPDto>().HasNoKey();
        modelBuilder.Entity<GetExistingKMAmalyateKhakiInfoWithBarAvordDto>().HasNoKey();

        modelBuilder.Entity<clsOperationHasAddedOperations>()
            .HasOne(x => x.Operation)
            .WithMany()
            .HasForeignKey(x => x.OperationId)
            .OnDelete(DeleteBehavior.Restrict); // یا DeleteBehavior.NoAction

        modelBuilder.Entity<clsAppOperationInfoDetails>()
            .HasOne(x => x.AppQuestion) // یا اسم navigation property مربوطه
            .WithMany() // یا .WithMany(q => q.OperationInfoDetails) اگر داشته باشی
            .HasForeignKey(x => x.QuetionId)
            .OnDelete(DeleteBehavior.Restrict); // یا DeleteBehavior.NoAction

        modelBuilder.Entity<clsItemsHasCondition_ConditionContext>()
        .HasOne(x => x.ItemsHasCondition) // این اسم navigation property مربوطه‌ست
        .WithMany() // یا .WithMany(i => i.AddedToFBs) اگه تعریف شده
        .HasForeignKey(x => x.ItemsHasConditionId)
        .OnDelete(DeleteBehavior.Restrict); // یا DeleteBehavior.NoAction
      
        modelBuilder.Entity<clsItemsHasConditionAddedToFB>()
        .HasOne(x => x.ItemsHasCondition_ConditionContext) // این اسم navigation property مربوطه‌ست
        .WithMany() // یا .WithMany(i => i.AddedToFBs) اگه تعریف شده
        .HasForeignKey(x => x.ItemsHasCondition_ConditionContextId)
        .OnDelete(DeleteBehavior.Restrict); // یا DeleteBehavior.NoAction

    }
}
