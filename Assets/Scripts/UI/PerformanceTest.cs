using System;
using System.Diagnostics;

public static class PerformanceTest 
{
    public static double Tick(Action action)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke();

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T>(Action<T> action, T t)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke(t);

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T, U>(Action<T, U> action, T t, U u)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke(t, u);

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T, U, A>(Action<T, U, A> action, T t, U u, A a)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke(t, u, a);

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T, U, A, B>(Action<T, U, A, B> action, T t, U u, A a, B b)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke(t, u, a, b);

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T, U, A, B, C>(Action<T, U, A, B, C> action, T t, U u, A a, B b, C c)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke(t, u, a, b, c);

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T, U, A, B, C, D>(Action<T, U, A, B, C, D> action, T t, U u, A a, B b, C c, D d)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke(t, u, a, b, c, d);

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T>(Func<T> action)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke();

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T, U>(Func<T, U> action, T t)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke(t);

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T, U, A>(Func<T, U, A> action, T t, U u)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke(t, u);

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T, U, A, B>(Func<T, U, A, B> action, T t, U u, A a)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke(t, u, a);

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T, U, A, B, C>(Func<T, U, A, B, C> action, T t, U u, A a, B b)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke(t, u, a, b);

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T, U, A, B, C, D>(Func<T, U, A, B, C, D> action, T t, U u, A a, B b, C c)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke(t, u, a, b, c);

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static double Tick<T, U, A, B, C, D, E>(Func<T, U, A, B, C, D, E> action, T t, U u, A a, B b, C c, D d)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        action?.Invoke(t, u, a, b, c, d);

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}
