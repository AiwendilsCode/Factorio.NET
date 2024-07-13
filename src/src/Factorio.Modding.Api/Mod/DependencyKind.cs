using System.Runtime.Serialization;

namespace Factorio.Modding.Api.Mod
{
    public enum DependencyKind
    {
        /// <summary>
        /// !
        /// </summary>
        Incompatible,
        /// <summary>
        /// ?
        /// </summary>
        Optional,
        /// <summary>
        /// (?)
        /// </summary>
        HiddenOptional,
        /// <summary>
        /// ~
        /// </summary>
        NotAffectLoadOrder,
        Hard
    }
}
