﻿using acNET.Type;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Runtime.CompilerServices;

namespace acNET;

public partial class acAPI
{
    //internal T? GET<T>(string url, string? option=null,Header? head=null) where T : BaseBody
    //{
    //    try
    //    {
    //        if (!this.GetRequest(url, option??string.Empty, out string json,head)) return null;
    //        return Converter.ParsingJson<T>(json);
    //    }
    //    catch (Exception e)
    //    {
    //        if (e is acAPIError)
    //        {
    //            acAPIError error = (acAPIError)e;
    //            if (url.StartsWith("account"))
    //            {
    //                if (error.Code == 403)
    //                {
    //                    this.Errors.Enqueue(acAPIError.Create("권한이 없습니다. (또는 solvedacToken이 올바르지 않습니다.)", -403));
    //                    return null;
    //                }
    //                if (error.Code == 404)
    //                {
    //                    this.Errors.Enqueue(acAPIError.Create("리딤 코드가 올바르지 않습니다.",-404));
    //                    return null;
    //                }
    //            }
    //            else if(url.StartsWith("problem/lookup") && error.Code == 400)
    //            {
    //                this.Errors.Enqueue(acAPIError.Create("problemIds 를 잘못 입력한것 같습니다. 쉼표로 구분한 문제 ID 목록을 입력해야합니다.", -400));
    //                return null;
    //            }
    //        }
    //        this.Errors.Enqueue(e);
    //        return null;
    //    }
    //}
    internal T? GET<T>(string url,out acAPIError? error, string? option = null, Header? head = null) where T : BaseBody
    {
        //요청 실패시
        if (!this.GetRequest(url , option ?? string.Empty , out string json ,out var ex, head))
        {
            //만약 acAPI 에러가 아니라면, 큐에 삽입
            if ((error = (ex is acAPIError ace) ? ace : null) is null)
            {
                this.Errors.Enqueue(ex!);
            }
            return null;
        }
        //성공시
        error = null;
        return Converter.ParsingJson<T>(json);
    }
    internal List<T>? GETLIST<T>(string url, string? option = null) where T : BaseBody
    {
        try
        {
            if (!this.GetRequest(url, option ?? string.Empty, out string json,out var e)) return null;
            return Converter.ParsingJsonList<T>(json);
        }
        catch (Exception e)
        {
            this.Errors.Enqueue(e);
            return null;
        }
    }
    internal record Header(string key, string value);
    internal bool GetRequest(string url, string option, out string content,out Exception? error, Header? header = null)
    {
        try
        {
            //요청
            RestRequest request = new(url + option , Method.Get);
            if (header is Header head)
                request.AddHeader(head.key , head.value);
            var response = _client.Execute(request);
            //상태코드 점검
            if ((int)response.StatusCode < 200 || (int)response.StatusCode > 299)
            {
                content = string.Empty;
                error = acAPIError.Create("서버에서 반환에 실패했습니다." , (short)response.StatusCode);
                return false;
            }
            content = response.Content ?? string.Empty;
            error = null;
            return true;
        }
        catch (Exception ex)
        {
            content = string.Empty;
            if (ex is acAPIError ace)
            {
                //오류 추측
                if (url.StartsWith("account"))
                {
                    if (ace.Code == 403)
                    {
                        error = acAPIError.Create("권한이 없습니다. (또는 solvedacToken이 올바르지 않습니다.)" , -403);
                        return false;
                    }
                    if (ace.Code == 404)
                    {
                        error = acAPIError.Create("리딤 코드가 올바르지 않습니다." , -404);
                        return false;
                    }
                }
                else if (url.StartsWith("problem/lookup") && ace.Code == 400)
                {
                    error = acAPIError.Create("problemIds 를 잘못 입력한것 같습니다. 쉼표로 구분한 문제 ID 목록을 입력해야합니다." , -400);
                    return false;
                }
                //이외엔 그냥 오류코드라도
                error = ace;
                return false;
            }
            //acAPI가 아닌 오류
            error = ex;
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
