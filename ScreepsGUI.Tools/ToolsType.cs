using System;

namespace ScreepsGUI.Tools
{
    /// <summary>
    /// Classe d'utilitaires pour la gestion des Type
    /// </summary>
    public static class ToolsType
    {
#if !NET2

        /// <summary>
        /// Génére un objet vide
        /// </summary>
        /// <param name="type">Type de l'object</param>
        /// <returns>Object nouvellement instancié</returns>
        public static object GetDefault(this Type type)
#else

        /// <summary>
        /// Génére un objet vide
        /// </summary>
        /// <param name="type">Type de l'object</param>
        /// <returns>Object nouvellement instancié</returns>
        public static object GetDefault(Type type)
#endif
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }
    }
}