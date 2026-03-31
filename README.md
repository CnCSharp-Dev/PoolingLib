# PoolingLib

![Paint 3D](https://raw.githubusercontent.com/CnCSharp-Dev/PoolingLib/main/icon.png)

## 📖 English


### Overview
> PoolingLib is a lightweight object pooling library for .NET, reusing common objects to reduce memory allocations.
- `Get()` retrieves an object from the pool (creates if empty).
- `Return()` returns the object back to the pool.

### 📦 Supported Types

| Collection Pool | Base |
|-----------|-----------------|
| `ListPool<TObject>` | `List<TObject>` |
| `LinkedListPool<TObject>` | `LinkedList<TObject>` |
| `QueuePool<TObject>` | `Queue<TObject>` |
| `StackPool<TObject>` | `Stack<TObject>` |
| `ConcurrentBagPool<TObject>` | `ConcurrentBag<TObject>` |
| `CollectionPool<TObject>` | `Collection<TObject>` |
| `HashSetPool<TObject>` | `HashSet<TObject>` |
| `SortedSetPool<TObject>` | `SortedSet<TObject>` |

| Dictionary Pool | Base |
|-----------|-----------------|
| `DictionaryPool<TKey, TValue>` | `Dictionary<TKey, TValue>` |
| `SortedDictionaryPool<TKey, TValue>` | `SortedDictionary<TKey, TValue>` |

| Special Pool | Base |
|-----------|-----------------|
| `StringBuilderPool` | `StringBuilder` |
| `MemoryStreamPool` | `MemoryStream` |

### Usage Example
```csharp
// Get a pooled List<int>
var list = ListPool<int>.Pool.Get();

list.Add(1);
list.Add(4);
list.Remove(1);

Console.WriteLine(list.Count);   // Output: 1

// Return the list to the pool
ListPool<int>.Pool.Return(list);
```

---

## 📖 中文

### 简述
> PoolingLib 是一个用于.NET的轻量级对象池库，通过复用对象来减少内存分配。
- `Get()` 从池中获取一个对象 (如果为空则创建)。
- `Return()` 将对象返回至对象池。

### 📦 支持的类型

| 集合池 | 基类 |
|-----------|-----------------|
| `ListPool<TObject>` | `List<TObject>` |
| `LinkedListPool<TObject>` | `LinkedList<TObject>` |
| `QueuePool<TObject>` | `Queue<TObject>` |
| `StackPool<TObject>` | `Stack<TObject>` |
| `ConcurrentBagPool<TObject>` | `ConcurrentBag<TObject>` |
| `CollectionPool<TObject>` | `Collection<TObject>` |
| `HashSetPool<TObject>` | `HashSet<TObject>` |
| `SortedSetPool<TObject>` | `SortedSet<TObject>` |

| 字典池 | 基类 |
|-----------|-----------------|
| `DictionaryPool<TKey, TValue>` | `Dictionary<TKey, TValue>` |
| `SortedDictionaryPool<TKey, TValue>` | `SortedDictionary<TKey, TValue>` |

| 特殊池 | 基类 |
|-----------|-----------------|
| `StringBuilderPool` | `StringBuilder` |
| `MemoryStreamPool` | `MemoryStream` |

### 使用示例
```csharp
// 获取一个池化过的List<int>
var list = ListPool<int>.Pool.Get();

list.Add(1);
list.Add(4);
list.Remove(1);

Console.WriteLine(list.Count);   // 输出: 1

// 将List<int>返回至列表
ListPool<int>.Pool.Return(list);
```
