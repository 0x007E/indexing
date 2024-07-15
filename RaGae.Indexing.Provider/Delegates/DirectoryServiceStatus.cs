using RaGae.Indexing.Provider.Enumerations;

namespace RaGae.Indexing.Provider.Delegates
{
    public delegate void DirectoryServiceStatus(string data, int indentation = 0, ServiceStatusReply type = ServiceStatusReply.Directory);
}
