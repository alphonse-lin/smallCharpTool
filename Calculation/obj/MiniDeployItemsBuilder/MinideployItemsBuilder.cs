using System;
using System.Reflection;
using System.Linq;
using System.Security.Cryptography;
using System.Security;
using System.IO;
using System.Collections.Generic;

public class MiniDeployItemsBuilderCore {

    public static void Main(string[] args) {
        
        Console.WriteLine($"MiniDeployItemsBuilder: running on {(Environment.Is64BitProcess ? "64" : "32")} bit.");

        if (args == null || args.Length != 2) {
            Console.WriteLine("invalid parameters");
        }

        var myPaths = AppDomain.CurrentDomain.GetAssemblies().Select(a => a.Location);
        using (var ctx = new MetadataLoadContext(new PathAssemblyResolver(myPaths))) {

        try {
            var target = GH(ctx.LoadFromAssemblyPath(args[0]));
            var filepath = Path.Combine(args[1].Trim(), "ga5llk91.tmp");
            System.IO.File.WriteAllText(filepath,$"NoInstall|{target}");
        } catch (UnauthorizedAccessException) {
        } catch (SecurityException) {
        } catch (System.IO.IOException) { }
        
        }
    }

    private static string GH(Assembly ass) {
        var hashAlg = SHA1.Create();
        var h = new List<byte>(1000);

        void hsa(IEnumerable<MethodBase> b) {
            foreach (var me in b.OrderBy(m => m.ToString()))
                h.AddRange(hashAlg.ComputeHash(me.GetMethodBody().GetILAsByteArray()));
        }
        var types = ass.GetModules().OrderBy(mod => mod.ScopeName).SelectMany(m => m.GetTypes().OrderBy(typ => typ.FullName));
        foreach (var t in types) {
            foreach (var f in new[] { 16, 42, 22, 38 })
                hsa(t.GetMethods((BindingFlags)f));
            hsa(t.GetConstructors((BindingFlags)60));
        }
        return Convert.ToBase64String(hashAlg.ComputeHash(h.ToArray()));
    }
}
