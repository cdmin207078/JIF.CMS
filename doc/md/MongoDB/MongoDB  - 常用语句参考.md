# MongoDB - 常用语句参考

[TOC]



## 增



## 删



## 改



## 查

### 简单查询



#### query



#### distinct

**语法**

`db.collection.distinct(field, query, options)`

```js
db.crm_batch_call.distinct('batch_id',{'wordstemplate_id':10901})
// SQL:
// select distinct(batch_no) from crm_batch
// where wordstemplate_id = 10901
```

[db.collection.distinct() - 官方文档](https://docs.mongodb.com/manual/reference/method/db.collection.distinct/index.html#db-collection-distinct)



### 聚合查询



#### group



#### distinct

```js
db.crm_batch_call.aggregate([
    { $match: { 'wordstemplate_id':10901 } },
    { $group: {_id: '$batch_id'} }
])

// SQL:
// select batch_id from crm_batch_call
// where wordstemplate_id = 10901
// group by batch_id
```



#### count

```js
db.crm_batch_call.aggregate([
    { $match: {'wordstemplate_id':10901 } },
    { $group: {_id: '$batch_id', count: {$sum: 1} }  }
])

// SQL: 
// select batch_id, count(1) from crm_batch_call
// where wordstemplate_id = 10901
// group by batch_id
```



#### sum

```js
db.orders.aggregate([
   { $match: { status: "A" } },
   { $group: { _id: "$cust_id", total: { $sum: "$amount" } } }
])

// SQL:
// select cust_id, sum(amount) as total 
// from orders
// group by cust_id
```



#### having

```js
db.crm_batch_call.aggregate([
    { $match: { 'wordstemplate_id':10886 } },
    { $group: {_id: '$phone', count: {$sum: 1} }  },
    { $match: { 'count': {$gt: 1 } } }
])

// SQL:
// select count(1) from crm_batch_call
// group by phone
// having count(1) > 1
```



## 数据库状态查看





## 参考

[MongoDB 官方文档](https://docs.mongodb.com/manual/introduction/)