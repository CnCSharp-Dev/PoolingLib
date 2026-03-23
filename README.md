# PoolingLib


## 📖 English

### Overview
PoolingLib is a lightweight object pooling library for .NET, reusing common objects to reduce memory allocations.
- `Get()` retrieves an object from the pool (creates if empty).
- `Release()` returns the object back to the pool.

### 📦 Supported Types

| Pool Type | Base |
|-----------|-----------------|
| `ListPool<T>` | `List<T>` |
| `HashSetPool<T>` | `HashSet<T>` |
| `DictionaryPool<TKey, TValue>` | `Dictionary<TKey, TValue>` |
| `QueuePool<T>` | `Queue<T>` |
| `StackPool<T>` | `Stack<T>` |
| `StringBuilderPool` | `StringBuilder` |
| `MemoryStreamPool` | `MemoryStream` |
| `SortedSetPool<T>` | `SortedSet<T>` |
| `SortedDictionaryPool<TKey, TValue>` | `SortedDictionary<TKey, TValue>` |

### Usage Example
```csharp
// Get a pooled List<int>
var list = ListPool<int>.Pool.Get();

list.Add(1);
list.Add(4);
list.Remove(1);

Console.WriteLine(list.Count);   // Output: 1

// Return the list to the pool
ListPool<int>.Pool.Release(list);
```

---

## 📖 中文

### 简述
PoolingLib 是一个用于.NET的轻量级对象池库，通过复用对象来减少内存分配。
- `Get()` 从池中获取一个对象 (如果为空则创建)。
- `Release()` 将对象返回至对象池。

### 📦 支持的类型

| 池类型 | 基础类型 |
|-----------|-----------------|
| `ListPool<T>` | `List<T>` |
| `HashSetPool<T>` | `HashSet<T>` |
| `DictionaryPool<TKey, TValue>` | `Dictionary<TKey, TValue>` |
| `QueuePool<T>` | `Queue<T>` |
| `StackPool<T>` | `Stack<T>` |
| `StringBuilderPool` | `StringBuilder` |
| `MemoryStreamPool` | `MemoryStream` |
| `SortedSetPool<T>` | `SortedSet<T>` |
| `SortedDictionaryPool<TKey, TValue>` | `SortedDictionary<TKey, TValue>` |

### 使用示例
```csharp
// 获取一个池化过的List<int>
var list = ListPool<int>.Pool.Get();

list.Add(1);
list.Add(4);
list.Remove(1);

Console.WriteLine(list.Count);   // 输出: 1

// 将List<int>返回至列表
ListPool<int>.Pool.Release(list);
```
