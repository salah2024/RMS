using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class i30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "tblBaravordUser");

            migrationBuilder.EnsureSchema(
                name: "dbo");


            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "tblZaribRoadType",
                schema: "dbo",
                newName: "tblZaribRoadType");

            migrationBuilder.RenameTable(
                name: "tblZaribPishnahadiPaymanKarFosoul",
                schema: "dbo",
                newName: "tblZaribPishnahadiPaymanKarFosoul");

            migrationBuilder.RenameTable(
                name: "tblZaribPishnahadiPaymanKar",
                schema: "dbo",
                newName: "tblZaribPishnahadiPaymanKar");

            migrationBuilder.RenameTable(
                name: "tblZaribBalaSari",
                schema: "dbo",
                newName: "tblZaribBalaSari");

            migrationBuilder.RenameTable(
                name: "tblZarayebTabdil",
                schema: "dbo",
                newName: "tblZarayebTabdil");

            migrationBuilder.RenameTable(
                name: "tblZarayebFosoul",
                schema: "dbo",
                newName: "tblZarayebFosoul");

            migrationBuilder.RenameTable(
                name: "tblWorkflowStepUser",
                schema: "dbo",
                newName: "tblWorkflowStepUser");

            migrationBuilder.RenameTable(
                name: "tblWorkflowStepRole",
                schema: "dbo",
                newName: "tblWorkflowStepRole");

            migrationBuilder.RenameTable(
                name: "tblWorkflowStep",
                schema: "dbo",
                newName: "tblWorkflowStep");

            migrationBuilder.RenameTable(
                name: "tblWorkflowInstance",
                schema: "dbo",
                newName: "tblWorkflowInstance");

            migrationBuilder.RenameTable(
                name: "tblWorkflowHistory",
                schema: "dbo",
                newName: "tblWorkflowHistory");

            migrationBuilder.RenameTable(
                name: "tblWorkflowAssignment",
                schema: "dbo",
                newName: "tblWorkflowAssignment");

            migrationBuilder.RenameTable(
                name: "tblWorkflowAction",
                schema: "dbo",
                newName: "tblWorkflowAction");

            migrationBuilder.RenameTable(
                name: "tblWorkflow",
                schema: "dbo",
                newName: "tblWorkflow");

            migrationBuilder.RenameTable(
                name: "tblVahed",
                schema: "dbo",
                newName: "tblVahed");

            migrationBuilder.RenameTable(
                name: "tblUserNotification",
                schema: "dbo",
                newName: "tblUserNotification");

            migrationBuilder.RenameTable(
                name: "tblUserDetail",
                schema: "dbo",
                newName: "tblUserDetail");

            migrationBuilder.RenameTable(
                name: "tblTajmieFosoulItems",
                schema: "dbo",
                newName: "tblTajmieFosoulItems");

            migrationBuilder.RenameTable(
                name: "tblTajmieFosoul",
                schema: "dbo",
                newName: "tblTajmieFosoul");

            migrationBuilder.RenameTable(
                name: "tblTajmieFBItems",
                schema: "dbo",
                newName: "tblTajmieFBItems");

            migrationBuilder.RenameTable(
                name: "tblTajhizKargahDarsad",
                schema: "dbo",
                newName: "tblTajhizKargahDarsad");

            migrationBuilder.RenameTable(
                name: "tblTadilIndexNoeFB",
                schema: "dbo",
                newName: "tblTadilIndexNoeFB");

            migrationBuilder.RenameTable(
                name: "tblTadilIndexFosoul",
                schema: "dbo",
                newName: "tblTadilIndexFosoul");

            migrationBuilder.RenameTable(
                name: "tblSubItemsAddingToFB",
                schema: "dbo",
                newName: "tblSubItemsAddingToFB");

            migrationBuilder.RenameTable(
                name: "tblShomarehFBForQuesForAbnieFani",
                schema: "dbo",
                newName: "tblShomarehFBForQuesForAbnieFani");

            migrationBuilder.RenameTable(
                name: "tblShahr",
                schema: "dbo",
                newName: "tblShahr");

            migrationBuilder.RenameTable(
                name: "tblSetting",
                schema: "dbo",
                newName: "tblSetting");

            migrationBuilder.RenameTable(
                name: "tblSegmentsFromGEODB",
                schema: "dbo",
                newName: "tblSegmentsFromGEODB");

            migrationBuilder.RenameTable(
                name: "tblRolePermission",
                schema: "dbo",
                newName: "tblRolePermission");

            migrationBuilder.RenameTable(
                name: "tblRizMetreUsersHistory",
                schema: "dbo",
                newName: "tblRizMetreUsersHistory");

            migrationBuilder.RenameTable(
                name: "tblRizMetreUsers",
                schema: "dbo",
                newName: "tblRizMetreUsers");

            migrationBuilder.RenameTable(
                name: "tblRizMetreStar",
                schema: "dbo",
                newName: "tblRizMetreStar");

            migrationBuilder.RenameTable(
                name: "tblRizMetreForBarAvordAddedBoardStand",
                schema: "dbo",
                newName: "tblRizMetreForBarAvordAddedBoardStand");

            migrationBuilder.RenameTable(
                name: "tblRizMetreForBarAvordAddedBoard",
                schema: "dbo",
                newName: "tblRizMetreForBarAvordAddedBoard");

            migrationBuilder.RenameTable(
                name: "tblRizKiloMetrazhOfHaml",
                schema: "dbo",
                newName: "tblRizKiloMetrazhOfHaml");

            migrationBuilder.RenameTable(
                name: "tblQuesForAbnieFaniValues",
                schema: "dbo",
                newName: "tblQuesForAbnieFaniValues");

            migrationBuilder.RenameTable(
                name: "tblQuesForAbnieFani",
                schema: "dbo",
                newName: "tblQuesForAbnieFani");

            migrationBuilder.RenameTable(
                name: "tblProjectCalendar",
                schema: "dbo",
                newName: "tblProjectCalendar");

            migrationBuilder.RenameTable(
                name: "tblProject",
                schema: "dbo",
                newName: "tblProject");

            migrationBuilder.RenameTable(
                name: "tblPolVaAbroBarAvordRizMetre",
                schema: "dbo",
                newName: "tblPolVaAbroBarAvordRizMetre");

            migrationBuilder.RenameTable(
                name: "tblPolVaAbroBarAvord",
                schema: "dbo",
                newName: "tblPolVaAbroBarAvord");

            migrationBuilder.RenameTable(
                name: "tblPermission",
                schema: "dbo",
                newName: "tblPermission");

            migrationBuilder.RenameTable(
                name: "tblPayKaniInfoForBarAvordMore",
                schema: "dbo",
                newName: "tblPayKaniInfoForBarAvordMore");

            migrationBuilder.RenameTable(
                name: "tblPayKaniInfoForBarAvordEzafeBahaRizMetre",
                schema: "dbo",
                newName: "tblPayKaniInfoForBarAvordEzafeBahaRizMetre");

            migrationBuilder.RenameTable(
                name: "tblPayKaniInfoForBarAvordEzafeBaha",
                schema: "dbo",
                newName: "tblPayKaniInfoForBarAvordEzafeBaha");

            migrationBuilder.RenameTable(
                name: "tblPayKaniInfoForBarAvordDetailsRizMetre",
                schema: "dbo",
                newName: "tblPayKaniInfoForBarAvordDetailsRizMetre");

            migrationBuilder.RenameTable(
                name: "tblPayKaniInfoForBarAvordDetailsMore",
                schema: "dbo",
                newName: "tblPayKaniInfoForBarAvordDetailsMore");

            migrationBuilder.RenameTable(
                name: "tblPayKaniInfoForBarAvordDetails",
                schema: "dbo",
                newName: "tblPayKaniInfoForBarAvordDetails");

            migrationBuilder.RenameTable(
                name: "tblPayKaniInfoForBarAvord",
                schema: "dbo",
                newName: "tblPayKaniInfoForBarAvord");

            migrationBuilder.RenameTable(
                name: "tblOstan",
                schema: "dbo",
                newName: "tblOstan");

            migrationBuilder.RenameTable(
                name: "tblOperationsOfHamlAndItems",
                schema: "dbo",
                newName: "tblOperationsOfHamlAndItems");

            migrationBuilder.RenameTable(
                name: "tblOperationsOfHaml_ItemsHasHaml",
                schema: "dbo",
                newName: "tblOperationsOfHaml_ItemsHasHaml");

            migrationBuilder.RenameTable(
                name: "tblOperationsOfHaml",
                schema: "dbo",
                newName: "tblOperationsOfHaml");

            migrationBuilder.RenameTable(
                name: "tblOperationHasAddedOperationsType",
                schema: "dbo",
                newName: "tblOperationHasAddedOperationsType");

            migrationBuilder.RenameTable(
                name: "tblOperationHasAddedOperationsLevelNumber",
                schema: "dbo",
                newName: "tblOperationHasAddedOperationsLevelNumber");

            migrationBuilder.RenameTable(
                name: "tblOperationHasAddedOperations",
                schema: "dbo",
                newName: "tblOperationHasAddedOperations");

            migrationBuilder.RenameTable(
                name: "tblOperationDetail",
                schema: "dbo",
                newName: "tblOperationDetail");

            migrationBuilder.RenameTable(
                name: "tblOperationAmelMoaser",
                schema: "dbo",
                newName: "tblOperationAmelMoaser");

            migrationBuilder.RenameTable(
                name: "tblOperation_ItemsFB",
                schema: "dbo",
                newName: "tblOperation_ItemsFB");

            migrationBuilder.RenameTable(
                name: "tblOperation",
                schema: "dbo",
                newName: "tblOperation");

            migrationBuilder.RenameTable(
                name: "tblNoeKhakBardariEzafeBaha",
                schema: "dbo",
                newName: "tblNoeKhakBardariEzafeBaha");

            migrationBuilder.RenameTable(
                name: "tblNoeKhakBardari_NoeKhakBardariEzafeBaha",
                schema: "dbo",
                newName: "tblNoeKhakBardari_NoeKhakBardariEzafeBaha");

            migrationBuilder.RenameTable(
                name: "tblNoeKhakBardari",
                schema: "dbo",
                newName: "tblNoeKhakBardari");

            migrationBuilder.RenameTable(
                name: "tblNiroZaribKarKard",
                schema: "dbo",
                newName: "tblNiroZaribKarKard");

            migrationBuilder.RenameTable(
                name: "tblNiroMasalehMashin",
                schema: "dbo",
                newName: "tblNiroMasalehMashin");

            migrationBuilder.RenameTable(
                name: "tblMenuPermission",
                schema: "dbo",
                newName: "tblMenuPermission");

            migrationBuilder.RenameTable(
                name: "tblMenuItem",
                schema: "dbo",
                newName: "tblMenuItem");

            migrationBuilder.RenameTable(
                name: "tblMashinType",
                schema: "dbo",
                newName: "tblMashinType");

            migrationBuilder.RenameTable(
                name: "tblKiloMetrazhOfHaml",
                schema: "dbo",
                newName: "tblKiloMetrazhOfHaml");

            migrationBuilder.RenameTable(
                name: "tblKhakRiziItem",
                schema: "dbo",
                newName: "tblKhakRiziItem");

            migrationBuilder.RenameTable(
                name: "tblKhakRiziEzafeBahasRizMetre",
                schema: "dbo",
                newName: "tblKhakRiziEzafeBahasRizMetre");

            migrationBuilder.RenameTable(
                name: "tblKhakRiziEzafeBaha",
                schema: "dbo",
                newName: "tblKhakRiziEzafeBaha");

            migrationBuilder.RenameTable(
                name: "tblKhakRiziCondition",
                schema: "dbo",
                newName: "tblKhakRiziCondition");

            migrationBuilder.RenameTable(
                name: "tblKhakRiziBarAvordRizMetre",
                schema: "dbo",
                newName: "tblKhakRiziBarAvordRizMetre");

            migrationBuilder.RenameTable(
                name: "tblKhakRiziBarAvord",
                schema: "dbo",
                newName: "tblKhakRiziBarAvord");

            migrationBuilder.RenameTable(
                name: "tblItemsRelatedToItemHaml",
                schema: "dbo",
                newName: "tblItemsRelatedToItemHaml");

            migrationBuilder.RenameTable(
                name: "tblItemsHasHaml",
                schema: "dbo",
                newName: "tblItemsHasHaml");

            migrationBuilder.RenameTable(
                name: "tblItemsHasConditionAddedToFBRizMetre",
                schema: "dbo",
                newName: "tblItemsHasConditionAddedToFBRizMetre");

            migrationBuilder.RenameTable(
                name: "tblItemsHasConditionAddedToFB",
                schema: "dbo",
                newName: "tblItemsHasConditionAddedToFB");

            migrationBuilder.RenameTable(
                name: "tblItemsHasCondition_ConditionContext",
                schema: "dbo",
                newName: "tblItemsHasCondition_ConditionContext");

            migrationBuilder.RenameTable(
                name: "tblItemsHasCondition",
                schema: "dbo",
                newName: "tblItemsHasCondition");

            migrationBuilder.RenameTable(
                name: "tblItemsForGetValues",
                schema: "dbo",
                newName: "tblItemsForGetValues");

            migrationBuilder.RenameTable(
                name: "tblItemsFields",
                schema: "dbo",
                newName: "tblItemsFields");

            migrationBuilder.RenameTable(
                name: "tblItemsFBShomarehValueShomareh",
                schema: "dbo",
                newName: "tblItemsFBShomarehValueShomareh");

            migrationBuilder.RenameTable(
                name: "tblItemsFBDependQuestionForAbnieFani",
                schema: "dbo",
                newName: "tblItemsFBDependQuestionForAbnieFani");

            migrationBuilder.RenameTable(
                name: "tblItemsAddingToFB",
                schema: "dbo",
                newName: "tblItemsAddingToFB");

            migrationBuilder.RenameTable(
                name: "tblItemFBStar",
                schema: "dbo",
                newName: "tblItemFBStar");

            migrationBuilder.RenameTable(
                name: "tblHadAksarErtefaKoole",
                schema: "dbo",
                newName: "tblHadAksarErtefaKoole");

            migrationBuilder.RenameTable(
                name: "tblGeneralProjectTiming",
                schema: "dbo",
                newName: "tblGeneralProjectTiming");

            migrationBuilder.RenameTable(
                name: "tblFosoul",
                schema: "dbo",
                newName: "tblFosoul");

            migrationBuilder.RenameTable(
                name: "tblFehrestBahaItemsReplace",
                schema: "dbo",
                newName: "tblFehrestBahaItemsReplace");

            migrationBuilder.RenameTable(
                name: "tblFehrestBaha",
                schema: "dbo",
                newName: "tblFehrestBaha");

            migrationBuilder.RenameTable(
                name: "tblFB",
                schema: "dbo",
                newName: "tblFB");

            migrationBuilder.RenameTable(
                name: "tblEzafeBahaKhakRiziAddItems",
                schema: "dbo",
                newName: "tblEzafeBahaKhakRiziAddItems");

            migrationBuilder.RenameTable(
                name: "tblEzafeBahaKhakRizi",
                schema: "dbo",
                newName: "tblEzafeBahaKhakRizi");

            migrationBuilder.RenameTable(
                name: "tblDastakPolInfo",
                schema: "dbo",
                newName: "tblDastakPolInfo");

            migrationBuilder.RenameTable(
                name: "tblConditionGroup",
                schema: "dbo",
                newName: "tblConditionGroup");

            migrationBuilder.RenameTable(
                name: "tblConditionContext",
                schema: "dbo",
                newName: "tblConditionContext");

            migrationBuilder.RenameTable(
                name: "tblCompanyManagementHistory",
                schema: "dbo",
                newName: "tblCompanyManagementHistory");

            migrationBuilder.RenameTable(
                name: "tblCompany",
                schema: "dbo",
                newName: "tblCompany");

            migrationBuilder.RenameTable(
                name: "tblBoardStandItems",
                schema: "dbo",
                newName: "tblBoardStandItems");

            migrationBuilder.RenameTable(
                name: "tblBoardItems",
                schema: "dbo",
                newName: "tblBoardItems");

            migrationBuilder.RenameTable(
                name: "tblBoardInfo",
                schema: "dbo",
                newName: "tblBoardInfo");

            migrationBuilder.RenameTable(
                name: "tblBaseInfoType",
                schema: "dbo",
                newName: "tblBaseInfoType");

            migrationBuilder.RenameTable(
                name: "tblBaseInfo",
                schema: "dbo",
                newName: "tblBaseInfo");

            migrationBuilder.RenameTable(
                name: "tblBaravordZaribManteghe",
                schema: "dbo",
                newName: "tblBaravordZaribManteghe");

            migrationBuilder.RenameTable(
                name: "tblBaravordZaribBalaSari",
                schema: "dbo",
                newName: "tblBaravordZaribBalaSari");

            migrationBuilder.RenameTable(
                name: "tblBaravordWorkflow",
                schema: "dbo",
                newName: "tblBaravordWorkflow");

            migrationBuilder.RenameTable(
                name: "tblBarAvordUserTajmie",
                schema: "dbo",
                newName: "tblBarAvordUserTajmie");

            migrationBuilder.RenameTable(
                name: "tblBaravordUserAccess",
                schema: "dbo",
                newName: "tblBaravordUserAccess");

            migrationBuilder.RenameTable(
                name: "tblBaravordUser",
                schema: "dbo",
                newName: "tblBaravordUser");

            migrationBuilder.RenameTable(
                name: "tblBarAvordPaymanKar",
                schema: "dbo",
                newName: "tblBarAvordPaymanKar");

            migrationBuilder.RenameTable(
                name: "tblBarAvordMoshaver",
                schema: "dbo",
                newName: "tblBarAvordMoshaver");

            migrationBuilder.RenameTable(
                name: "tblBarAvordKarfarma",
                schema: "dbo",
                newName: "tblBarAvordKarfarma");

            migrationBuilder.RenameTable(
                name: "tblBarAvordHamlRizMetre",
                schema: "dbo",
                newName: "tblBarAvordHamlRizMetre");

            migrationBuilder.RenameTable(
                name: "tblBarAvordHamlNecessaryLimit",
                schema: "dbo",
                newName: "tblBarAvordHamlNecessaryLimit");

            migrationBuilder.RenameTable(
                name: "tblBarAvordHaml",
                schema: "dbo",
                newName: "tblBarAvordHaml");

            migrationBuilder.RenameTable(
                name: "tblBarAvordAddedBoardStand",
                schema: "dbo",
                newName: "tblBarAvordAddedBoardStand");

            migrationBuilder.RenameTable(
                name: "tblBarAvordAddedBoard",
                schema: "dbo",
                newName: "tblBarAvordAddedBoard");

            migrationBuilder.RenameTable(
                name: "tblBakhsh",
                schema: "dbo",
                newName: "tblBakhsh");

            migrationBuilder.RenameTable(
                name: "tblBahayeVahedItemsRelated",
                schema: "dbo",
                newName: "tblBahayeVahedItemsRelated");

            migrationBuilder.RenameTable(
                name: "tblAppQuestion",
                schema: "dbo",
                newName: "tblAppQuestion");

            migrationBuilder.RenameTable(
                name: "tblAppOperationInfoMain",
                schema: "dbo",
                newName: "tblAppOperationInfoMain");

            migrationBuilder.RenameTable(
                name: "tblAppOperationInfoDetails",
                schema: "dbo",
                newName: "tblAppOperationInfoDetails");

            migrationBuilder.RenameTable(
                name: "tblAnalizBaha",
                schema: "dbo",
                newName: "tblAnalizBaha");

            migrationBuilder.RenameTable(
                name: "tblAmalyateKhakiInfoForBarAvordMore",
                schema: "dbo",
                newName: "tblAmalyateKhakiInfoForBarAvordMore");

            migrationBuilder.RenameTable(
                name: "tblAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre",
                schema: "dbo",
                newName: "tblAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre");

            migrationBuilder.RenameTable(
                name: "tblAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre",
                schema: "dbo",
                newName: "tblAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre");

            migrationBuilder.RenameTable(
                name: "tblAmalyateKhakiInfoForBarAvordEzafeBaha",
                schema: "dbo",
                newName: "tblAmalyateKhakiInfoForBarAvordEzafeBaha");

            migrationBuilder.RenameTable(
                name: "tblAmalyateKhakiInfoForBarAvordDetailsRizMetre",
                schema: "dbo",
                newName: "tblAmalyateKhakiInfoForBarAvordDetailsRizMetre");

            migrationBuilder.RenameTable(
                name: "tblAmalyateKhakiInfoForBarAvordDetailsMore",
                schema: "dbo",
                newName: "tblAmalyateKhakiInfoForBarAvordDetailsMore");

            migrationBuilder.RenameTable(
                name: "tblAmalyateKhakiInfoForBarAvordDetails",
                schema: "dbo",
                newName: "tblAmalyateKhakiInfoForBarAvordDetails");

            migrationBuilder.RenameTable(
                name: "tblAmalyateKhakiInfoForBarAvord",
                schema: "dbo",
                newName: "tblAmalyateKhakiInfoForBarAvord");

            migrationBuilder.RenameTable(
                name: "tblAbroDaliDetails",
                schema: "dbo",
                newName: "tblAbroDaliDetails");

            migrationBuilder.RenameTable(
                name: "tblAbadeKoole",
                schema: "dbo",
                newName: "tblAbadeKoole");

            migrationBuilder.RenameTable(
                name: "tblAbadDivarBali",
                schema: "dbo",
                newName: "tblAbadDivarBali");

            migrationBuilder.RenameTable(
                name: "QuesForAbnieFaniValuesDto",
                schema: "dbo",
                newName: "QuesForAbnieFaniValuesDto");

            migrationBuilder.RenameTable(
                name: "OperationNameAnalizBahaWithOperationDto",
                schema: "dbo",
                newName: "OperationNameAnalizBahaWithOperationDto");

            migrationBuilder.RenameTable(
                name: "ItemsFBShomarehValueShomarehUpdateProcedureDto",
                schema: "dbo",
                newName: "ItemsFBShomarehValueShomarehUpdateProcedureDto");

            migrationBuilder.RenameTable(
                name: "ItemsFBDependQuestionForAbnieFaniForSPDto",
                schema: "dbo",
                newName: "ItemsFBDependQuestionForAbnieFaniForSPDto");

            migrationBuilder.RenameTable(
                name: "GetOperationNameHasValueDto",
                schema: "dbo",
                newName: "GetOperationNameHasValueDto");

            migrationBuilder.RenameTable(
                name: "GetExistingKMPayKaniInfoWithBarAvordDto",
                schema: "dbo",
                newName: "GetExistingKMPayKaniInfoWithBarAvordDto");

            migrationBuilder.RenameTable(
                name: "GetExistingKMAmalyateKhakiInfoWithBarAvordDto",
                schema: "dbo",
                newName: "GetExistingKMAmalyateKhakiInfoWithBarAvordDto");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "dbo",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "dbo",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "dbo",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "dbo",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "dbo",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "dbo",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "dbo",
                newName: "AspNetRoleClaims");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "tblBaravordUser",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
