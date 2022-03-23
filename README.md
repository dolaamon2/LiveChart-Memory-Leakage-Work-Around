# LiveChart-Memory-Leakage-Work-Around
This repository use MultipleSeriesTest example in https://github.com/Live-Charts/GearedExamples to test memory leakage problem in geared version.
In this example memory leakage appears when I put two charts in stackpanel. For solving this problem I use some work around in the code.

Here are the result before using work around
![res](testresult/res.png?raw=true)

After
![hackres](testresult/hackres.png?raw=true)
