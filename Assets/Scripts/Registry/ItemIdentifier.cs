using System.Text;

namespace Assets.Scripts.Registry
{
    public class ItemIdentifier
    {
        public readonly Domain Domain;
        public readonly string TableName;
        public readonly string InnerName;
        public readonly string IdentifierString;

        public ItemIdentifier(Domain domain, string tableName, string innerName)
        {
            this.Domain = domain;
            this.TableName = tableName;
            this.InnerName = innerName;
            this.IdentifierString = CreateIdentifierString(domain, tableName, innerName);
        }

        public static string CreateIdentifierString(Domain domain, string tableName, string innerName)
        {
            return domain.Name + ":" + tableName + "." + innerName;
        }

        public static ItemIdentifier Parse(string identifier, Domain domain = null, string tableName = null)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                return null;
            }

            Domain finalDomain;
            int colonIndex;
            if ((colonIndex = identifier.IndexOf(':')) == -1)
            {
                if (domain == null)
                {
                    throw new System.Exception("Domain cannot be undefined");
                }
                else
                {
                    finalDomain = domain;
                }
            }
            else
            {
                finalDomain = Domain.FindByName(identifier.Substring(0, colonIndex));
            }

            string finalTableName;
            int dotIndex;
            if ((dotIndex = identifier.IndexOf('.')) == -1)
            {
                if (tableName == null)
                {
                    throw new System.Exception("Table name cannot be undefined");
                }
                else
                {
                    finalTableName = tableName;
                }
            }
            else
            {
                finalTableName = identifier.Substring(colonIndex + 1, dotIndex - colonIndex);
            }

            return new ItemIdentifier(
                finalDomain,
                finalTableName,
                identifier.Substring(dotIndex + 1));
        }
    }
}
