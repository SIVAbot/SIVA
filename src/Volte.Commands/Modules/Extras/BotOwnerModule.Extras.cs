using System.Net.Http;
using Volte.Services;

namespace Volte.Commands.Modules
{
    public sealed partial class BotOwnerModule
    {
        public EvalService Eval { get; set; }
        public HttpClient Http { get; set; }
        public CacheService Cache { get; set; }
    }
}