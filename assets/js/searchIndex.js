
var camelCaseTokenizer = function (obj) {
    var previous = '';
    return obj.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
        var current = cur.toLowerCase();
        if(acc.length === 0) {
            previous = current;
            return acc.concat(current);
        }
        previous = previous.concat(current);
        return acc.concat([current, previous]);
    }, []);
}
lunr.tokenizer.registerFunction(camelCaseTokenizer, 'camelCaseTokenizer')
var searchModule = function() {
    var idMap = [];
    function y(e) { 
        idMap.push(e); 
    }
    var idx = lunr(function() {
        this.field('title', { boost: 10 });
        this.field('content');
        this.field('description', { boost: 5 });
        this.field('tags', { boost: 50 });
        this.ref('id');
        this.tokenizer(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
    });
    function a(e) { 
        idx.add(e); 
    }

    a({
        id:0,
        title:"HttpClientAliases",
        content:"HttpClientAliases",
        description:'',
        tags:''
    });

    a({
        id:1,
        title:"CakeHttpClientHandler",
        content:"CakeHttpClientHandler",
        description:'',
        tags:''
    });

    a({
        id:2,
        title:"HttpSettings",
        content:"HttpSettings",
        description:'',
        tags:''
    });

    a({
        id:3,
        title:"HttpSettingsExtensions",
        content:"HttpSettingsExtensions",
        description:'',
        tags:''
    });

    y({
        url:'/Cake.Http/Cake.Http/api/Cake.Http/HttpClientAliases',
        title:"HttpClientAliases",
        description:""
    });

    y({
        url:'/Cake.Http/Cake.Http/api/Cake.Http/CakeHttpClientHandler',
        title:"CakeHttpClientHandler",
        description:""
    });

    y({
        url:'/Cake.Http/Cake.Http/api/Cake.Http/HttpSettings',
        title:"HttpSettings",
        description:""
    });

    y({
        url:'/Cake.Http/Cake.Http/api/Cake.Http/HttpSettingsExtensions',
        title:"HttpSettingsExtensions",
        description:""
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
