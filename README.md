# ASP.NET-Core-RESTfulAPI-Swagger 
示範在.net core中建置API版本控制與Swagger UI 產生API文件  
可修改apiVersion調用不同版本API，如:v1.0、v1.1、v2.0

### API規格
<table>
<tbody><tr><th>方法</th>
<th>網址</th>
<th>描述</th>
<th>要求文本</th>
<th>回應文本</th>
</tr><tr>
<td>GET</td>
<td><code>{apiVersion}/api/todo</code></td>
<td>取得所有待辦事項</td>
<td>無</td>
<td>待辦事項陣列</td>
</tr>
<tr>
<td>GET</td>
<td><code>{apiVersion}/api/todo/{id}</code></td>
<td>依識別碼取得待辦事項</td>
<td>無</td>
<td>待辦事項</td>
</tr>
<tr>
<td>POST</td>
<td><code>{apiVersion}/api/todo</code></td>
<td>新增待辦事項</td>
<td>待辦事項</td>
<td>待辦事項</td>
</tr>
<tr>
<td>PUT</td>
<td><code>{apiVersion}/api/todo/{id}</code></td>
<td>更新現有的待辦事項</td>
<td>待辦事項</td>
<td>待辦事項</td>
</tr>
<tr>
<td>DELETE</td>
<td><code>{apiVersion}/api/todo/{id}</code></td>
<td>刪除待辦事項</td>
<td>無</td>
<td>待辦事項</td>
</tr>
<tr>
<td>GET</td>
<td><code>{apiVersion}/api/version</code></td>
<td>取得API版本</td>
<td>無</td>
<td>API版本</td>
</tr>
</tbody></table>


### DEMO

<img src="demo.gif">
