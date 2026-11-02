using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Controllers.AmalyateKhaki.Dto;
using RMS.Controllers.KhakRizi.EnumKhakRizi;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;
using RMS.Models.StoredProceduresData;
using System.Reflection;
using System.Security;

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
    public DbSet<clsPayKaniInfoForBarAvord> PayKaniInfoForBarAvords { get; set; }
    public DbSet<clsPayKaniInfoForBarAvordDetails> PayKaniInfoForBarAvordDetailses { get; set; }
    public DbSet<clsPayKaniInfoForBarAvordEzafeBaha> PayKaniInfoForBarAvordEzafeBahas { get; set; }
    public DbSet<clsPayKaniInfoForBarAvordDetailsMore> PayKaniInfoForBarAvordDetailsMores { get; set; }
    public DbSet<clsPayKaniInfoForBarAvordMore> PayKaniInfoForBarAvordMores { get; set; }
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
    public DbSet<clsRizMetreUsersHistory> RizMetreUsersHistories { get; set; }
    public DbSet<clsSegmentsFromGEODB> SegmentsFromGEODBs { get; set; }
    public DbSet<clsShomarehFBForQuesForAbnieFani> ShomarehFBForQuesForAbnieFanis { get; set; }
    public DbSet<clsSubItemsAddingToFB> SubItemsAddingToFBs { get; set; }
    public DbSet<clsZarayebTabdil> ZarayebTabdils { get; set; }
    public DbSet<clsZaribRoadType> ZaribRoadTypes { get; set; }
    //public DbSet<clsNoeFosoul> NoeFosouls { get; set; }
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
    public DbSet<clsPayKaniInfoForBarAvordDetailsRizMetre> PayKaniInfoForBarAvordDetailsRizMetres { get; set; }
    public DbSet<clsPayKaniInfoForBarAvordEzafeBahaRizMetre> PayKaniInfoForBarAvordEzafeBahaRizMetres { get; set; }
    public DbSet<clsNoeKhakBardariEzafeBaha> NoeKhakBardariEzafeBahas { get; set; }
    public DbSet<clsNoeKhakBardari_NoeKhakBardariEzafeBaha> NoeKhakBardari_NoeKhakBardariEzafeBahas { get; set; }
    public DbSet<clsKhakRiziCondition> KhakRiziConditions { get; set; }
    public DbSet<clsKhakRiziItem> KhakRiziItems { get; set; }
    public DbSet<clsKhakRiziBarAvord> KhakRiziBarAvords { get; set; }
    public DbSet<clsKhakRiziBarAvordRizMetre> KhakRiziBarAvordRizMetres { get; set; }
    public DbSet<clsEzafeBahaKhakRizi> EzafeBahaKhakRizis { get; set; }
    public DbSet<clsEzafeBahaKhakRiziAddItems> EzafeBahaKhakRiziAddItemses { get; set; }
    public DbSet<clsKhakRiziEzafeBaha> KhakRiziEzafeBahas { get; set; }
    public DbSet<clsKhakRiziEzafeBahaRizMetre> KhakRiziEzafeBahaRizMetres { get; set; }
    public DbSet<clsItemsRelatedToItemHaml> ItemsRelatedToItemHamls { get; set; }
    public DbSet<clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre> AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetres { get; set; }
    public DbSet<clsBarAvordHaml> BarAvordHamls { get; set; }
    public DbSet<clsBarAvordHamlRizMetre> BarAvordHamlRizMetres { get; set; }
    public DbSet<clsOperationDetail> OperationDetails { get; set; }
    public DbSet<clsBarAvordHamlNecessaryLimit> BarAvordHamlNecessaryLimits { get; set; }
    public DbSet<clsUserDetail> UserDetails { get; set; }
    public DbSet<clsCompany> Companies { get; set; }
    public DbSet<clsCompanyManagementHistory> CompanyManagementHistories { get; set; }
    public DbSet<clsZaribBalaSari> ZaribBalaSaris { get; set; }
    public DbSet<clsOstan> Ostans { get; set; }
    public DbSet<clsShahr> Shahrs { get; set; }
    public DbSet<clsBakhsh> Bakhshs { get; set; }
    public DbSet<clsBaravordZaribManteghe> BaravordZaribManteghes { get; set; }
    public DbSet<clsBaravordZaribBalaSari> BaravordZaribBalaSaris { get; set; }
    public DbSet<clsVahed> Vaheds { get; set; }
    public DbSet<clsItemFBStar> ItemFBStars { get; set; }
    public DbSet<clsRizMetreStar> RizMetreStars { get; set; }
    public DbSet<clsPolVaAbroBarAvordRizMetre> PolVaAbroBarAvordRizMetres { get; set; }
    public DbSet<clsPermission> Permissions { get; set; }
    public DbSet<clsRolePermission> RolePermissions { get; set; }
    public DbSet<clsMenuItem> MenuItems { get; set; }
    public DbSet<clsMenuPermission> MenuPermissions { get; set; }
    public DbSet<clsTajhizKargahDarsad> TajhizKargahDarsads { get; set; }
    public DbSet<clsSetting> Setting { get; set; }
    public DbSet<clsZarayebFosoul> ZarayebFosouls { get; set; }
    public DbSet<clsFehrestBahaItemsReplace> FehrestBahaItemsReplaces { get; set; }
    public DbSet<clsZaribPishnahadiPaymanKar> ZaribPishnahadiPaymanKars { get; set; }
    public DbSet<clsZaribPishnahadiPaymanKarFosoul> ZaribPishnahadiPaymanKarFosouls { get; set; }
    public DbSet<clsUserNotification> UserNotifications { get; set; }
    public DbSet<clsTadilIndexNoeFB> TadilIndexNoeFBs { get; set; }
    public DbSet<clsTadilIndexFosoul> TadilIndexFosouls { get; set; }
    public DbSet<clsWorkflow> Workflows { get; set; }
    public DbSet<clsWorkflowStep> WorkflowSteps { get; set; }
    public DbSet<clsWorkflowStepRole> WorkflowStepRoles { get; set; }
    public DbSet<clsWorkflowStepUser> WorkflowStepUsers { get; set; }
    public DbSet<clsWorkflowInstance> WorkflowInstances { get; set; }
    public DbSet<clsWorkflowAssignment> WorkflowAssignments { get; set; }
    public DbSet<clsWorkflowHistory> WorkflowHistories { get; set; }
    public DbSet<clsBaravordWorkflow> BaravordWorkflows { get; set; }
    public DbSet<clsBaravordUserAccess> BaravordUserAccesses { get; set; }
    public DbSet<clsBarAvordUserTajmie> BarAvordUserTajmies { get; set; }
    public DbSet<clsTajmieFBItems> TajmieFBItemses { get; set; }
    public DbSet<clsTajmieFosoulItems> TajmieFosoulItemses { get; set; }
    public DbSet<clsTajmieFosoul> TajmieFosouls { get; set; }
    public DbSet<clsBahayeVahedItemsRelated> BahayeVahedItemsRelateds { get; set; }
    public DbSet<clsItemsHasConditionAddedToFBRizMetre> ItemsHasConditionAddedToFBRizMetres { get; set; }
    public DbSet<clsBarAvordMoshaver> BarAvordMoshavers { get; set; }
    public DbSet<clsBarAvordKarfarma> BarAvordKarfarmas { get; set; }
    public DbSet<clsBarAvordPaymanKar> BarAvordPaymanKars { get; set; }
    public DbSet<clsItemHasConditionBaseItemRemoved> ItemHasConditionBaseItemRemoveds { get; set; }
    public DbSet<clsBarAvordProjectionWorkFlow> BarAvordProjectionWorkFlows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(clsBaseEntity).IsAssignableFrom(entityType.ClrType)
                && !entityType.ClrType.IsAbstract)
            {
                var method = typeof(ApplicationDbContext)
                    .GetMethod(
                        nameof(ApplyIsDeletedFilter),
                        BindingFlags.NonPublic | BindingFlags.Static);

                var genericMethod = method!
                    .MakeGenericMethod(entityType.ClrType);

                genericMethod.Invoke(null, new object[]
                {
                    modelBuilder
                });
            }
        }


        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<clsFehrestBaha>()
            .HasIndex(x => new { x.Sal, x.NoeFB })
            .HasDatabaseName("IX_FehrestBahas_Sal_NoeFB");

        modelBuilder.Entity<clsFehrestBahaItemsReplace>()
            .HasIndex(x => x.FBShomarehMain)
            .HasDatabaseName("IX_FehrestBahaItemsReplaces_FBShomarehMain");

        modelBuilder.Entity<clsOperation>()
            .HasIndex(o => new { o.Id, o.Year })
            .IsUnique(); // ترکیب Id + Year باید یکتا باشه

        modelBuilder.Entity<clsOperation>()
            .HasOne(o => o.Parent)
            .WithMany(p => p.Children)
            .HasForeignKey(o => o.ParentId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<clsUserDetail>()
            .HasOne(d => d.User)
            .WithOne(u => u.UserDetail)      // اگر در ApplicationUser این پراپرتی رو داری
            .HasForeignKey<clsUserDetail>(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<QuesForAbnieFaniValuesDto>().HasNoKey();
        modelBuilder.Entity<OperationNameAnalizBahaWithOperationDto>().HasNoKey();
        modelBuilder.Entity<GetOperationNameHasValueDto>().HasNoKey();

        modelBuilder.Entity<ItemsFBShomarehValueShomarehUpdateProcedureDto>().HasNoKey();
        modelBuilder.Entity<ItemsFBDependQuestionForAbnieFaniForSPDto>().HasNoKey();
        modelBuilder.Entity<GetExistingKMAmalyateKhakiInfoWithBarAvordDto>().HasNoKey();
        modelBuilder.Entity<GetExistingKMPayKaniInfoWithBarAvordDto>().HasNoKey();

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

        modelBuilder.Entity<clsWorkflowAssignment>()
            .HasOne(x => x.WorkflowStep)
            .WithMany()
            .HasForeignKey(x => x.WorkflowStepId)
            .OnDelete(DeleteBehavior.Restrict);

        // Permission
        modelBuilder.Entity<clsPermission>(e =>
        {
            e.HasIndex(x => x.Key).IsUnique();
            e.Property(x => x.Key).IsRequired().HasMaxLength(200);
            e.Property(x => x.Title).IsRequired().HasMaxLength(200);
            e.Property(x => x.Group).HasMaxLength(100);
        });

        // RolePermission (many-to-many)
        modelBuilder.Entity<clsRolePermission>(e =>
        {
            e.HasKey(x => new { x.RoleId, x.PermissionId });

            e.HasOne(x => x.Role)
             .WithMany()
             .HasForeignKey(x => x.RoleId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Permission)
             .WithMany(p => p.RolePermissions)
             .HasForeignKey(x => x.PermissionId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // MenuItem (self-referencing)
        modelBuilder.Entity<clsMenuItem>(e =>
        {
            e.Property(x => x.Title).IsRequired().HasMaxLength(200);
            e.Property(x => x.Url).HasMaxLength(400);
            e.Property(x => x.Icon).HasMaxLength(100);

            e.HasOne(x => x.Parent)
             .WithMany(x => x.Children)
             .HasForeignKey(x => x.ParentId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // MenuPermission (many-to-many)
        modelBuilder.Entity<clsMenuPermission>(e =>
        {
            e.HasKey(x => new { x.MenuItemId, x.PermissionId });

            e.HasOne(x => x.MenuItem)
             .WithMany(m => m.MenuPermissions)
             .HasForeignKey(x => x.MenuItemId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Permission)
             .WithMany(p => p.MenuPermissions)
             .HasForeignKey(x => x.PermissionId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<clsWorkflowInstance>()
            .HasOne(x => x.CurrentStep)
            .WithMany()
            .HasForeignKey(x => x.CurrentStepId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<clsBaravordUser>()
        .HasQueryFilter(x => x.IsDeleted != true);
    }

    private static void ApplyIsDeletedFilter<TEntity>(
        ModelBuilder modelBuilder)
        where TEntity : clsBaseEntity
    {
        modelBuilder.Entity<TEntity>()
            .HasQueryFilter(x => x.IsDeleted != true);
    }
}
