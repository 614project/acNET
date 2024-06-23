﻿using acNET.Type;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Runtime.CompilerServices;

namespace acNET;

public partial class acAPI
{
    internal T? GET<T>(string url, string? option=null,Header? head=null) where T : BaseBody
    {
        try
        {
            if (!this.GetRequest(url, option??string.Empty, out string json,head)) return null;
            return Converter.ParsingJson<T>(json);
        }
        catch (Exception e)
        {
            if (e is acAPIError)
            {
                acAPIError error = (acAPIError)e;
                if (url.StartsWith("account"))
                {
                    if (error.Code == 403)
                    {
                        this.Errors.Enqueue(acAPIError.Create("권한이 없습니다. (또는 solvedacToken이 올바르지 않습니다.)", -403));
                        return null;
                    }
                    if (error.Code == 404)
                    {
                        this.Errors.Enqueue(acAPIError.Create("리딤 코드가 올바르지 않습니다.",-404));
                        return null;
                    }
                }
                else if(url.StartsWith("problem/lookup") && error.Code == 400)
                {
                    this.Errors.Enqueue(acAPIError.Create("problemIds 를 잘못 입력한것 같습니다. 쉼표로 구분한 문제 ID 목록을 입력해야합니다.", -400));
                    return null;
                }
            }
            this.Errors.Enqueue(e);
            return null;
        }
    }
    internal List<T>? GETLIST<T>(string url, string? option = null) where T : BaseBody
    {
        try
        {
            if (!this.GetRequest(url, option ?? string.Empty, out string json)) return null;
            return Converter.ParsingJsonList<T>(json);
        }
        catch (Exception e)
        {
            this.Errors.Enqueue(e);
            return null;
        }
    }
    internal record Header(string key, string value);
    internal bool GetRequest(string url, string option, out string content,Header? header = null)
    {
        RestRequest request = new(url + option , Method.Get);
        if (header is Header head)
            request.AddHeader(head.key , head.value);
            var response = _client.Execute(request);
        if ((int)response.StatusCode < 200 || (int)response.StatusCode > 299)
        {
            throw acAPIError.Create("서버에서 반환에 실패했습니다.",(short)response.StatusCode);
        }
        content = response.Content;
        return true;

    }
    [Obsolete("C#이 쓰지말레요")]
    internal static bool SaveImage(string url, string filename, out string fullname)
    {
        try
        {
            using (WebClient webClient = new WebClient())
            {
                int dot = url.LastIndexOf('.');
                fullname = filename;
                if (dot >= 0)
                {
                    fullname += url.Substring(dot);
                }
                webClient.DownloadFile(url, fullname);
            }
            return true;
        }
        catch (Exception e)
        {
            fullname = e.Message;
            return false;
        }
    }
    internal static bool SaveFile(string url,string savepath)
    {
        using (RestClient client = new())
        {
            if (client.DownloadData(new(url)) is byte[] binary)
            {
                File.WriteAllBytes(savepath , binary);
                return true;
            }
        }
        return false;
    }
}
