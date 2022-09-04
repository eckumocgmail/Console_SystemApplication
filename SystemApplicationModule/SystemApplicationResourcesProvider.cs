
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
public interface ISystemApplicationResourcesProvider
{
    public Task<MethodResult<List<string>>> GetHtmlTemplateNames();
    public Task<MethodResult<IDictionary<string,string>>> ReadHtmlTemplateText(string name);
    public void ShowHtmlTemplate(string name);
}
 
public class SystemApplicationResourcesProvider: Cmd, ISystemApplicationResourcesProvider
{
    public SystemApplicationResourcesProvider(string Dir)
    {
        this.Dir = Dir;
    }

    private string Dir { get; set; }

    public async Task<MethodResult<List<string>>> GetHtmlTemplateNames()
    {
        try
        {
            var result = Exec($"dir /b {Dir}").Split("\r\n").Where(test => String.IsNullOrWhiteSpace(test) == false).ToList();
            await Task.CompletedTask;
            return MethodResult<List<string>>.OnResult(null);
        }
        catch (Exception ex)
        {
            return MethodResult<List<string>>.OnError(ex);
        }
         
    }

    public Task<MethodResult<IDictionary<string, string>>> ReadHtmlTemplateText(string name)
    {
        throw new NotImplementedException();
    }

    public void ShowHtmlTemplate(string name)
    {
        throw new NotImplementedException();
    }
}