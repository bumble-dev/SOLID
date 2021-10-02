using System;
using jsonBlog;
using jsonBlog.Models;
using System.Collections.Generic;
using System.Diagnostics.Contracts;




public class PostStorageSql : IPostManager 
{

    private Dictionary<int, string> database = new Dictionary<int, string>();
    private readonly string cnx;
    private readonly PostSerializer _serializer;

    public PostStorageSql(string cnx, PostSerializer serializer)
    {
        Contract.Requires<ArgumentNullException>(string.IsNullOrWhiteSpace(cnx));
        this.cnx = cnx;
        this._serializer = serializer;
    }

    public Post Load(User author, int id)
    {
        // open connexion
        var post = database[id];
        Contract.Ensures(post != null);
        Contract.Invariant(cnx != null);
        var json = database[id];
        return _serializer.DeserializePost(json);
    }

    public Post Save( Post post)
    {
        var json = _serializer.SerializePost(post);
        database.Add(post.ID, json);
        return post;
    }

    

}