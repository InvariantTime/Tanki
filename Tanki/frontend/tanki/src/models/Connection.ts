import {HubConnection, HubConnectionBuilder, LogLevel} from "@microsoft/signalr";

const hubUrl = "http://localhost:5074/ws/rooms";

export class Connection {
    
    private connection: HubConnection;
    private roomListChanged: (event: () => void) => void;

    public IsConnected: boolean;

    public get RoomListChanged() {
         return this.roomListChanged;
    }

    constructor() {
        this.connection = new HubConnectionBuilder()
                    .withUrl(hubUrl)
                    .withAutomaticReconnect()
                    .configureLogging(LogLevel.Information)
                    .build();

        this.roomListChanged = (event) => {
            this.connection.on("OnRoomsChanged", event);
        }

        this.IsConnected = false;
    }

    public async Connect() {
        await this.connection.start();
        this.IsConnected = true;
    }

    public async Disconnect() {
        await this.connection.stop();
        this.IsConnected = false;
    }
}

export const CreateRoomListConnection = () => {
    var result = new Connection();
    return result;
}