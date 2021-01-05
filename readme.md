# readme

本项目是使用C#实现六角格地图编辑器的尝试。目的在于显示大量对象时要比python快。

## 第一阶段

第一阶段目的是制作一个窗体，包含一个画板，若干个按钮。通过按钮实现生成六角格地图，调整大小和全局透明度。

c#的图形程序绘图的一般流程：
储存图像的Image/Bitmap
绘制图像的Graphics
- Graphics可以从Image/Bitmap创建，也可从图板直接创建。
- 图板的绘图是一次性的，如果擦除一部分会不分图层地擦除。

## tips

c#不同长度的整数不能直接转换。
对画板使用invalidate（“否定”）来触发Paint事件。处理paint事件进行重画，这样内容才能在面板上保留下来。
可以全部或部分地否定画板内容。

### 全局透明度的更改法-复习线性代数：ColorMatrix

通过乘以一个变换矩阵，可以快速将一整个图面的ARGB向量变换成我需要的形状。认为ARGB向量第五位是1，构造5x5矩阵：

[
 ka, 0,0,0,0
 0,kr,0,0,0
 0,0,kg,0,0
 0,0,0,kb,0
 a,r,g,b,1
]

原结果变为 (ka*ain+a)，其他分量亦同。要求不变则对应位写1，要求重置则对角线对应位写0

### 颜色的查找和替换：ColorMap

## drawImage 的重载

        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destPoints:
        //     由三个 System.Drawing.Point 结构组成的数组，这三个结构定义一个平行四边形。
        //
        //   srcRect:
        //     System.Drawing.Rectangle 结构，它指定 image 对象中要绘制的部分。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit);
        //
        // 摘要:
        //     在指定的位置使用原始物理大小绘制指定的 System.Drawing.Image。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   point:
        //     System.Drawing.PointF 结构，它指定所绘制图像的左上角。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, PointF point);
        //
        // 摘要:
        //     在指定的位置使用原始物理大小绘制指定的 System.Drawing.Image。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   x:
        //     所绘制图像的左上角的 x 坐标。
        //
        //   y:
        //     所绘制图像的左上角的 y 坐标。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, float x, float y);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   rect:
        //     System.Drawing.RectangleF 结构，它指定所绘制图像的位置和大小。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, RectangleF rect);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   x:
        //     所绘制图像的左上角的 x 坐标。
        //
        //   y:
        //     所绘制图像的左上角的 y 坐标。
        //
        //   width:
        //     所绘制图像的宽度。
        //
        //   height:
        //     所绘制图像的高度。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, float x, float y, float width, float height);
        //
        // 摘要:
        //     在由坐标对指定的位置，使用图像的原始物理大小绘制指定的图像。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   x:
        //     所绘制图像的左上角的 x 坐标。
        //
        //   y:
        //     所绘制图像的左上角的 y 坐标。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, int x, int y);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   rect:
        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Rectangle rect);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   x:
        //     所绘制图像的左上角的 x 坐标。
        //
        //   y:
        //     所绘制图像的左上角的 y 坐标。
        //
        //   width:
        //     所绘制图像的宽度。
        //
        //   height:
        //     所绘制图像的高度。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, int x, int y, int width, int height);
        //
        // 摘要:
        //     在指定位置并且按指定形状和大小绘制指定的 System.Drawing.Image。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destPoints:
        //     由三个 System.Drawing.PointF 结构组成的数组，这三个结构定义一个平行四边形。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, PointF[] destPoints);
        //
        // 摘要:
        //     在指定位置并且按指定形状和大小绘制指定的 System.Drawing.Image。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destPoints:
        //     由三个 System.Drawing.Point 结构组成的数组，这三个结构定义一个平行四边形。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Point[] destPoints);
        //
        // 摘要:
        //     在指定的位置绘制图像的一部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   x:
        //     所绘制图像的左上角的 x 坐标。
        //
        //   y:
        //     所绘制图像的左上角的 y 坐标。
        //
        //   srcRect:
        //     System.Drawing.RectangleF 结构，它指定 System.Drawing.Image 中要绘制的部分。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, float x, float y, RectangleF srcRect, GraphicsUnit srcUnit);
        //
        // 摘要:
        //     在指定的位置绘制图像的一部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   x:
        //     所绘制图像的左上角的 x 坐标。
        //
        //   y:
        //     所绘制图像的左上角的 y 坐标。
        //
        //   srcRect:
        //     System.Drawing.Rectangle 结构，它指定 image 对象中要绘制的部分。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destRect:
        //     System.Drawing.RectangleF 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。
        //
        //   srcRect:
        //     System.Drawing.RectangleF 结构，它指定 image 对象中要绘制的部分。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destRect:
        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。
        //
        //   srcRect:
        //     System.Drawing.Rectangle 结构，它指定 image 对象中要绘制的部分。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destPoints:
        //     由三个 System.Drawing.PointF 结构组成的数组，这三个结构定义一个平行四边形。
        //
        //   srcRect:
        //     System.Drawing.RectangleF 结构，它指定 image 对象中要绘制的部分。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destPoints:
        //     由三个 System.Drawing.PointF 结构组成的数组，这三个结构定义一个平行四边形。
        //
        //   srcRect:
        //     System.Drawing.RectangleF 结构，它指定 image 对象中要绘制的部分。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
        //
        //   imageAttr:
        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destPoints:
        //     由三个 System.Drawing.PointF 结构组成的数组，这三个结构定义一个平行四边形。
        //
        //   srcRect:
        //     System.Drawing.RectangleF 结构，它指定 image 对象中要绘制的部分。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
        //
        //   imageAttr:
        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
        //
        //   callback:
        //     System.Drawing.Graphics.DrawImageAbort 委托，它指定在绘制图像期间要调用的方法。 此方法被频繁调用以检查是否根据应用程序确定的条件停止
        //     System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)
        //     方法的执行。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, DrawImageAbort callback);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destRect:
        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。
        //
        //   srcX:
        //     要绘制的源图像部分的左上角的 x 坐标。
        //
        //   srcY:
        //     要绘制的源图像部分的左上角的 y 坐标。
        //
        //   srcWidth:
        //     要绘制的源图像部分的宽度。
        //
        //   srcHeight:
        //     要绘制的源图像部分的高度。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定用于确定源矩形的度量单位。
        //
        //   imageAttrs:
        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
        //
        //   callback:
        //     System.Drawing.Graphics.DrawImageAbort 委托，它指定在绘制图像期间要调用的方法。 此方法被频繁调用以检查是否根据应用程序确定的条件停止
        //     System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.IntPtr)
        //     方法的执行。
        //
        //   callbackData:
        //     一个值，它为 System.Drawing.Graphics.DrawImageAbort 委托指定在检查是否停止执行 DrawImage 方法时要使用的附加数据。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, DrawImageAbort callback, IntPtr callbackData);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destRect:
        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。
        //
        //   srcX:
        //     要绘制的源图像部分的左上角的 x 坐标。
        //
        //   srcY:
        //     要绘制的源图像部分的左上角的 y 坐标。
        //
        //   srcWidth:
        //     要绘制的源图像部分的宽度。
        //
        //   srcHeight:
        //     要绘制的源图像部分的高度。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定用于确定源矩形的度量单位。
        //
        //   imageAttr:
        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destRect:
        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。
        //
        //   srcX:
        //     要绘制的源图像部分的左上角的 x 坐标。
        //
        //   srcY:
        //     要绘制的源图像部分的左上角的 y 坐标。
        //
        //   srcWidth:
        //     要绘制的源图像部分的宽度。
        //
        //   srcHeight:
        //     要绘制的源图像部分的高度。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定用于确定源矩形的度量单位。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destRect:
        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。
        //
        //   srcX:
        //     要绘制的源图像部分的左上角的 x 坐标。
        //
        //   srcY:
        //     要绘制的源图像部分的左上角的 y 坐标。
        //
        //   srcWidth:
        //     要绘制的源图像部分的宽度。
        //
        //   srcHeight:
        //     要绘制的源图像部分的高度。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定用于确定源矩形的度量单位。
        //
        //   imageAttrs:
        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
        //
        //   callback:
        //     System.Drawing.Graphics.DrawImageAbort 委托，它指定在绘制图像期间要调用的方法。 此方法被频繁调用以检查是否根据应用程序确定的条件停止
        //     System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.IntPtr)
        //     方法的执行。
        //
        //   callbackData:
        //     一个值，它为 System.Drawing.Graphics.DrawImageAbort 委托指定在检查是否停止执行 DrawImage 方法时要使用的附加数据。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, DrawImageAbort callback, IntPtr callbackData);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destRect:
        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。
        //
        //   srcX:
        //     要绘制的源图像部分的左上角的 x 坐标。
        //
        //   srcY:
        //     要绘制的源图像部分的左上角的 y 坐标。
        //
        //   srcWidth:
        //     要绘制的源图像部分的宽度。
        //
        //   srcHeight:
        //     要绘制的源图像部分的高度。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定用于确定源矩形的度量单位。
        //
        //   imageAttrs:
        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
        //
        //   callback:
        //     System.Drawing.Graphics.DrawImageAbort 委托，它指定在绘制图像期间要调用的方法。 此方法被频繁调用以检查是否根据应用程序确定的条件停止
        //     System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)
        //     方法的执行。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, DrawImageAbort callback);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destRect:
        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。
        //
        //   srcX:
        //     要绘制的源图像部分的左上角的 x 坐标。
        //
        //   srcY:
        //     要绘制的源图像部分的左上角的 y 坐标。
        //
        //   srcWidth:
        //     要绘制的源图像部分的宽度。
        //
        //   srcHeight:
        //     要绘制的源图像部分的高度。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定用于确定源矩形的度量单位。
        //
        //   imageAttrs:
        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destRect:
        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。
        //
        //   srcX:
        //     要绘制的源图像部分的左上角的 x 坐标。
        //
        //   srcY:
        //     要绘制的源图像部分的左上角的 y 坐标。
        //
        //   srcWidth:
        //     要绘制的源图像部分的宽度。
        //
        //   srcHeight:
        //     要绘制的源图像部分的高度。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定用于确定源矩形的度量单位。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destPoints:
        //     由三个 System.Drawing.PointF 结构组成的数组，这三个结构定义一个平行四边形。
        //
        //   srcRect:
        //     System.Drawing.Rectangle 结构，它指定 image 对象中要绘制的部分。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
        //
        //   imageAttr:
        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
        //
        //   callback:
        //     System.Drawing.Graphics.DrawImageAbort 委托，它指定在绘制图像期间要调用的方法。 此方法被频繁调用以检查是否根据应用程序确定的条件停止
        //     System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)
        //     方法的执行。
        //
        //   callbackData:
        //     一个值，它为 System.Drawing.Graphics.DrawImageAbort 委托指定在检查是否停止执行 System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)
        //     方法时要使用的附加数据。
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, DrawImageAbort callback, int callbackData);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destPoints:
        //     由三个 System.Drawing.PointF 结构组成的数组，这三个结构定义一个平行四边形。
        //
        //   srcRect:
        //     System.Drawing.Rectangle 结构，它指定 image 对象中要绘制的部分。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
        //
        //   imageAttr:
        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
        //
        //   callback:
        //     System.Drawing.Graphics.DrawImageAbort 委托，它指定在绘制图像期间要调用的方法。 此方法被频繁调用以检查是否根据应用程序确定的条件停止
        //     System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)
        //     方法的执行。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, DrawImageAbort callback);
        //
        // 摘要:
        //     在指定位置绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destPoints:
        //     由三个 System.Drawing.Point 结构组成的数组，这三个结构定义一个平行四边形。
        //
        //   srcRect:
        //     System.Drawing.Rectangle 结构，它指定 image 对象中要绘制的部分。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
        //
        //   imageAttr:
        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destPoints:
        //     由三个 System.Drawing.PointF 结构组成的数组，这三个结构定义一个平行四边形。
        //
        //   srcRect:
        //     System.Drawing.RectangleF 结构，它指定 image 对象中要绘制的部分。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
        //
        //   imageAttr:
        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
        //
        //   callback:
        //     System.Drawing.Graphics.DrawImageAbort 委托，它指定在绘制图像期间要调用的方法。 此方法被频繁调用以检查是否根据应用程序确定的条件停止
        //     System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)
        //     方法的执行。
        //
        //   callbackData:
        //     一个值，它为 System.Drawing.Graphics.DrawImageAbort 委托指定在检查是否停止执行 System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)
        //     方法时要使用的附加数据。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, DrawImageAbort callback, int callbackData);
        //
        // 摘要:
        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   destRect:
        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。
        //
        //   srcX:
        //     要绘制的源图像部分的左上角的 x 坐标。
        //
        //   srcY:
        //     要绘制的源图像部分的左上角的 y 坐标。
        //
        //   srcWidth:
        //     要绘制的源图像部分的宽度。
        //
        //   srcHeight:
        //     要绘制的源图像部分的高度。
        //
        //   srcUnit:
        //     System.Drawing.GraphicsUnit 枚举的成员，它指定用于确定源矩形的度量单位。
        //
        //   imageAttr:
        //     System.Drawing.Imaging.ImageAttributes，它指定 image 的重新着色和伽玛信息。
        //
        //   callback:
        //     System.Drawing.Graphics.DrawImageAbort 委托，它指定在绘制图像期间要调用的方法。 此方法被频繁调用以检查是否根据应用程序确定的条件停止
        //     System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)
        //     方法的执行。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr, DrawImageAbort callback);
        //
        // 摘要:
        //     在指定的位置使用原始物理大小绘制指定的 System.Drawing.Image。
        //
        // 参数:
        //   image:
        //     要绘制的 System.Drawing.Image。
        //
        //   point:
        //     System.Drawing.Point 结构，它表示所绘制图像的左上角的位置。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     image 为 null。
        public void DrawImage(Image image, Point point);