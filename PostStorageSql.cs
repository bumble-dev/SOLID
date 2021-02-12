using System;
using jsonBlog;
using jsonBlog.Models;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

public class PostStorageSql : PostStorage
{

    private Dictionary<int, string> database = new Dictionary<int, string>();
    private readonly string cnx;

    public PostStorageSql(string cnx)
    {
        Contract.Requires<ArgumentNullException>(string.IsNullOrWhiteSpace(cnx));
        this.cnx = cnx;
    }

    public override string Load(User author, int id)
    {
        // open connexion
        var post = database[id];
        Contract.Ensures(post != null);
        Contract.Invariant(cnx != null);
        return database[id];
    }

    public override string Save(string serializedPost, Post post)
    {
        database.Add(post.ID, serializedPost);
        return string.Empty;
    }

    public override string GetPath(User user, int id) => throw new NotImplementedException("Should ne be call");

}