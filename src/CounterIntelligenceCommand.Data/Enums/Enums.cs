using System.ComponentModel;

namespace CounterIntelligenceCommand.Data
{
    public enum Rank
    {
        [Description("General")]
        General,

        [Description("Lieutenant General")]
        LieutenantGeneral,

        [Description("Major General")]
        MajorGeneral,

        [Description("Brigadier General")]
        BrigadierGeneral,

        [Description("Colonel")]
        Colonel,

        [Description("Lieutenant Colonel")]
        LieutenantColonel,

        [Description("Major")]
        Major,

        [Description("Captain")]
        Captain,

        [Description("Lieutenant")]
        Lieutenant,

        [Description("Second Lieutenant")]
        SecondLieutenant
    }
}
