UI Manager dùng để quản lý các prefab UI

Ý tưởng xây dựng là khi bắt đầu game sẽ không load bất kỳ Canvas nào lên để tránh tốn hiệu năng,
khi bắt đầu mở một Canvas nào đó thì sẽ load và tạo Canvas đó, khi tắt đi thì sẽ deactive để sẵn sàng cho lần sau

Ưu điểm: 
+ phân tách từng Canvas riêng biệt rõ ràng
+ dễ sử dụng
+ tối ưu hơn về hiệu năng
+ khi cần sửa UI thì chỉ cần sửa prefab UI đó
+ có thể sử dụng trong hầu hết project cả project lớn

Yêu cầu:
+ cần tạo một canvas - Root và add UIManager vào, kéo canvas - root vào canvasparent
+ các canvas con sẽ có thể override sort order của convas - root
+ các prefab canvas sẽ cần để trong Resources/UI/
+ cần chú ý check lại các prefab canvas đặt Anchor present là scale all <góc dưới phải>
+ Canvas nào muốn dùng UIManager sẽ cần kế thừa UICanvas

Câu lệnh:
+ OpenUI<TênScript>(): mở canvas đó lên
+ CloseUI<TênScript>(): đóng canvas trực tiếp
+ CloseUI<TênScript>(float time): đóng canvas sau bao nhiêu giây
+ CloseAll(): đóng trực tiếp tất cả canvas -> trong trường hợp kết thúc game, lúc đó có thể người chơi đang mở setting hoặc gì đó
+ GetUI<TênScript>(): link trực tiếp đến Canvas đang mở
+ IsOpened<TênScript>(): check xem Canvas đó đang mở hay không
+ Một số hàm khi kế thừa UICanvas có thể sử dụng
 - public override void Setup(): hàm này sẽ được gọi trước frame vẽ hình Canvas đó lên
 - public override void Open(): hàm này sẽ được gọi sau frame vẽ hình Canvas đó lên
 - public override void Close(): đóng canvas sau ... giây
 - public override void CloseDirectly(): đóng trực tiếp canvas