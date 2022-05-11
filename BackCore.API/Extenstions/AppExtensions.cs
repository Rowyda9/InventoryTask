using BackCore.API.Middlewares;
using BackCore.BLL.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackCore.API.Extenstions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        }


        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
           app.UseMiddleware<ErrorHandlerMiddleware>();
        }


        public static void UseStaticFilesExtension(this IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                context.Context.Response.Headers.Add("Cache-Control", "public, max-age=2592000")
            });
            app.UseStaticFiles(); 

            app.UseResponseCompression();
        }
    }



}

