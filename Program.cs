using System;

class ProyectoMetodos
{
    static double f(double x, double y) => 2 * x - 3 * y + 1;

    static double ObtenerValorReal(double x) 
    {
        return (1.0 / 9.0) + (2.0 / 3.0) * x + (38.0 / 9.0) * Math.Exp(-3 * (x - 1));
    }

    static void Main()
    {
        EjecutarTarea1(0.1);

        EjecutarTarea1(0.05);

        EjecutarTarea2(0.1);

        EjecutarTarea3(0.1);

    }

    static void EjecutarTarea1(double h)

    {
        double x = 1.0;

        double yEuler = 5.0;

        double yEulerMej = 5.0;
        
        double xFinal = 1.5;


     //textos
        Console.WriteLine("\n--- TAREA 1: REPORTE DE ERRORES (h = " + h + ") ---");

        Console.WriteLine("xn\ty_Euler\ty_Mejorado\tValorReal\tErrorAbs\t%ErrorRel");

        while (x <= xFinal + (h / 10))
        {
            double vReal = ObtenerValorReal(x);
            
            // Cálculo automático de errores (basado en Euler Mejorado)
            double errAbs = Math.Abs(vReal - yEulerMej);
            double errRel = (vReal != 0) ? (errAbs / Math.Abs(vReal)) * 100 : 0;

            // Imprimimos todo de una vez
            Console.WriteLine(x.ToString("F2") + "\t" + 
                              yEuler.ToString("F4") + "\t" + 
                              yEulerMej.ToString("F4") + "\t" + 
                              vReal.ToString("F4") + "\t" + 
                              errAbs.ToString("F4") + "\t" + 
                              errRel.ToString("F2") + "%");

            // Método de Euler[cite: 1]
            yEuler += h * f(x, yEuler);

            // Método de Euler Mejorado[cite: 1]
            double k1 = f(x, yEulerMej);
            double k2 = f(x + h, yEulerMej + h * k1);
            yEulerMej += (h / 2.0) * (k1 + k2);

            x += h;
        }
    }
    
    static void EjecutarTarea2(double h)
    {
    double x = 1.0;
    double yRK4 = 5.0;
    double xFinal = 1.5;

    Console.WriteLine("\n--- TAREA 2: REPORTE RK4 (h = " + h + ") ---");
    Console.WriteLine("xn\ty_RK4\tValorReal\tErrorAbs\t%ErrorRel");

        while (x <= xFinal + (h / 10))
        {
          double vReal = ObtenerValorReal(x);
          double errAbs = Math.Abs(vReal - yRK4);
          double errRel = (vReal != 0) ? (errAbs / Math.Abs(vReal)) * 100 : 0;

          Console.WriteLine($"{x:F2}\t{yRK4:F4}\t{vReal:F4}\t{errAbs:F4}\t{errRel:F2}%");

          // Método RK4
          double k1 = f(x, yRK4);
          double k2 = f(x + h / 2.0, yRK4 + (h / 2.0) * k1);
          double k3 = f(x + h / 2.0, yRK4 + (h / 2.0) * k2);
          double k4 = f(x + h, yRK4 + h * k3);

          yRK4 += (h / 6.0) * (k1 + 2 * k2 + 2 * k3 + k4);
          x += h;
         }
    }
    static void EjecutarTarea3(double h)
    {
    
        double[] x = new double[5];
        double[] y = new double[5];
    
        // Función específica para Tarea 3: y' = 4x - 2y
        Func<double, double, double> f3 = (vx, vy) => 4 * vx - 2 * vy;

        x[0] = 0;
        y[0] = 2;

        Console.WriteLine("\n--- TAREA 3: ADAMS-BASHFORTH-MOULTON ---");
        Console.WriteLine("xn\tyn\tMétodo");
        Console.WriteLine($"{x[0]:F1}\t{y[0]:F4}\t(Dato inicial)");

        // PASO 1: Usar RK4 para obtener y1, y2 y y3
        for (int i = 0; i < 3; i++)
        {
            double k1 = f3(x[i], y[i]);
            double k2 = f3(x[i] + h / 2.0, y[i] + (h / 2.0) * k1);
            double k3 = f3(x[i] + h / 2.0, y[i] + (h / 2.0) * k2);
            double k4 = f3(x[i] + h, y[i] + h * k3);

            y[i + 1] = y[i] + (h / 6.0) * (k1 + 2 * k2 + 2 * k3 + k4);
            x[i + 1] = x[i] + h;
            Console.WriteLine($"{x[i+1]:F1}\t{y[i+1]:F4}\t(RK4)");
        }

         // PASO 2: Calcular y4 (y(0.4)) usando Adams-Bashforth-Moulton
        int n = 3; 
        // Predictor (Adams-Bashforth de 4 pasos)
        double y_pred = y[n] + (h / 24.0) * (55 * f3(x[n], y[n]) - 59 * f3(x[n-1], y[n-1]) + 37 * f3(x[n-2], y[n-2]) - 9 * f3(x[n-3], y[n-3]));
    
        double x_next = x[n] + h;

        // Corrector (Adams-Moulton de 4 pasos)
         double y_corr = y[n] + (h / 24.0) * (9 * f3(x_next, y_pred) + 19 * f3(x[n], y[n]) - 5 * f3(x[n-1], y[n-1]) + f3(x[n-2], y[n-2]));

        Console.WriteLine($"{x_next:F1}\t{y_corr:F4}\t(Adams-BM)");
    }
}
