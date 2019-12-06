
#  HttpReports
### �򵥽��� 
HttpReports �� .Net Core�µ�һ��Web����������� WebAPI ��Ŀ�� API ������Ŀ��ͨ���м������ʽ���ɵ�������Ŀ��, ͨ��HttpReports�������ÿ�����Ա���ٵĴ��һ�� API ���ܷ����Ļ���������վ��

 ![](https://raw.githubusercontent.com/SpringLeee/HttpReportsWeb/master/HttpReports.Web/wwwroot/Content/img/git/a3.png) 

��Ҫ���� HttpReports �м�� �� HttpReports.Web ������Ŀ��  

HttpReports�� https://github.com/SpringLeee/HttpReports 
  
HttpReports.Web�� https://github.com/SpringLeee/HttpReportsWeb

### ���ʹ��

##### 1.���� HttpReports.Web
 ��github���� HttpReports.Web ��Ŀ����Ŀ��ַ��https://github.com/SpringLeee/HttpReportsWeb, Web��Ŀ��.Net Core MVC ��Ŀ��ʹ������ʵ�֡�
 
 ![](https://raw.githubusercontent.com/SpringLeee/HttpReportsWeb/master/HttpReports.Web/wwwroot/Content/img/git/a1.png)
 
 
 
 ������ɺ���VS�д򿪣�Ȼ��ԭNuGet���������ɺ����� appsettings.json 
#### appsettings.json
```
{
  "ConnectionStrings": {
    "HttpReports": "Max Pool Size = 512;server=.;uid=sa;pwd=123456;database=HttpReports;"
  }, 
  "HttpReportsConfig": {
    "DBType": "SqlServer",
    "UserName": "admin",
    "Password": "123456"
  }
}

``` 
��Ҫ������
- HttpReports������һ�����õ������ַ�����
- DBType�����ݿ����ͣ�֧��SqlServer��MySql;
- UserName: Web��Ŀ�ĵ�¼����
- Password: Web��Ŀ�ĵ�¼���룻

��������ʹ�õ���SqlServer ���ݿ⣬��Ҫ������ConnectionStrings��Ȼ���ֶ��������ݿ� HttpReports��Web��Ŀ��������ݿ��Զ������������ڵ�һ�����е�ʱ��MockһЩ���� ��������ֱ��F5������Ŀ�� û������Ļ�����ֱ��������¼ҳ�棬�����û������� admin 123456����¼��Ӧ�ÿ��Կ��������ҳ��

 ![](https://raw.githubusercontent.com/SpringLeee/HttpReportsWeb/master/HttpReports.Web/wwwroot/Content/img/git/a3.png) 
 
���ڿ��Կ�����Ŀ�� auth,payment��sms ��������ڵ㣬����ڵ�Ķ������£�
 
�����ַ | ����ڵ�  | ˵�� 
-|-|-
https://www.abc.com/auth/api/user/login | auth  |
https://www.abc.com/log/api/user/login | log  |
https://www.abc.com/api/user/login | default  |  ���û��ǰ׺�Ļ�������default�ڵ�

��������Ŀ�ǵ���WebAPI��Ŀ����ô����ڵ�ֻ��һ�� default����������Ŀ�� GateWay ������Ŀ����ôWeb��Ŀ�Ϳ��Զ�ȡ���������ڵ㣬���� auth ��֤��payment֧���ȡ�


##### 2.��API��Ŀ��ʹ��

����Ҫɾ�� Web ��Ŀ��Mock���ݣ������ݿ� HttpReports���򿪱� RequestInfo,������ݣ�ִ��Sql
```
  Delete * From [HttpReports].[dbo].[RequestInfo]
```
###### �������ݿ������ַ���
HttpReports ���õ���API��Ŀ��������Ŀ������ʹ�� Ocelot������ĿΪ��.

���Ǵ�appsetting.json, �������ݿ������ַ�������Ҫ��Web��Ŀһ��

![](https://raw.githubusercontent.com/SpringLeee/HttpReportsWeb/master/HttpReports.Web/wwwroot/Content/img/git/a6.png)

###### Nuget����HttpReports

��װnuget�� **HttpReports** ����StartUp

��ConfigureServices ��������ӣ� 
```csharp
services.AddHttpReportsMiddlewire();
```
�����MySql���ݿ⣬����ӣ�
 ```csharp
 services.AddHttpReportsMiddlewire(options =>{ 
    options.DBType = DBType.MySql; 
 });
```

���뵽 Configure ���� ����Ҫ���� app.UseMVC() ���� app.UseOcelot().Wait() ��ǰ�棬Ҫ��Ȼ����Ч
```csharp
app.UseHttpReportsMiddlewire();
```
ConnectionStrings ���õ������ַ��������ݿ�����Ҫһ�£�ȫ��������Ժ����ǾͿ���ʹ�� Web ��Ŀ�ˡ�

### ��Ŀ��������Ҫ��

WebAPI����������Ŀ֧�ֵ�.Net Core �汾 2.2, 3.0, 3.1;

HttpReports.Web ��core�汾Ϊ 2.2  

### ��������

HttpReports �м�����첽���������Զ�api�ӿ������ʱ����Ժ��ԣ���������ʵ��ʹ�õ������ݿ�洢������Ҫע��ֱ���������ݿ��ѹ����

��������PostMan����һ���򵥲��ԣ�

WebAPI�ڵķ�����

```csharp
        public string Sql1()
        {
            SqlConnection con = new SqlConnection(
                "Max Pool Size = 512;server=.;uid=sa;pwd=123456;database=HyBasicData;");

            var list1 =  con.Query(" select * from [HyBasicData].[dbo].[Customers] ");

            var list2 = con.Query(" select * from [HyBasicData].[dbo].[Customers] ");

            var list3 = con.Query(" select * from [HyBasicData].[dbo].[Customers] "); 

            return list1.Count().ToString();
        } 
```
PostMan�ֱ������м���Ͳ�����м���� API���� 1000�Σ�ÿ300ms����һ��

 ˵�� | �������  | ƽ����Ӧʱ�� 
-|-|-
ԭ��API|1000|32.535
ʹ���м��|1000|32.899  

### �ܽ�

HttpReports ��ʵ��ԭ�������ӣ������������ WebAPI��Ŀ�����ٵ����һ�׷���ϵͳ ����ôʹ��HttpReports ��һ�������ѡ��

 
### ��ϵ����
 
 �������ʹ�ù�����������ʲô��������кõĽ���Ļ�����������ҵ�΢�ţ�ϣ�����԰�������
 ![](https://raw.githubusercontent.com/SpringLeee/HttpReportsWeb/master/HttpReports.Web/wwwroot/Content/img/git/a9.jpg)
 
 

 





