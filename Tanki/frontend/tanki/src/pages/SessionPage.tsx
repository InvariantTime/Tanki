import { GameField } from "../components/GameField";
import { SessionEditor } from "../components/SessionEditor";
import { PlayerList } from "../components/PlayerList";
import { useEffect, useState } from "react";
import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";
import { Player } from "../models/Player";
import { useNavigate, useParams } from "react-router-dom";
import { GameScene } from "../models/GameScene";
import { SceneData } from "../models/GameModels";

export const SessionPage = () => {

    const [connection, setConnection] = useState<HubConnection>();
    const [players, setPlayers] = useState<Player[]>([]);
    const { sessionId } = useParams<{sessionId:string}>();
    const [scene, setScene] = useState<GameScene>(new GameScene());
    const navigate = useNavigate();

    const onPlayerChanged = (players: Player[]) =>
    {
        setPlayers(players.sort((a, b) => a.score > b.score ? -1 : 1));
    }

    const onShutdown = (message: string) =>
    {
        alert(message);
        navigate("/");
    }

    const initConnection = () : HubConnection => {

        const builder = new HubConnectionBuilder()
            .withUrl(`http://localhost:5074/ws/session?sessionId=${sessionId}`)
            .configureLogging(LogLevel.Information);

        const connection = builder.build();

        connection.on("OnPlayersChanged", onPlayerChanged);
        connection.on("Shutdown", onShutdown);
        connection.on("VisualizeScene", visualizeScene);
        
        return connection;
    }

    const sendCode = async (code: string) : Promise<any> =>
    {
        return connection?.invoke("SendCode", code);
    }

    const visualizeScene = (data: SceneData) =>
    {
        scene.changeState(data);
    }

    useEffect(() => {

        if (!sessionId)
            return;

        const connection = initConnection();
        setConnection(connection);

        connection.start();

        return () => {
            if (connection.state === HubConnectionState.Connected)
                connection.stop();
        }
    }, [sessionId]);

    return (
        <div className="p-3 w-full">
            <div className="h-full flex justify-between
                 flex-wrap py-4 gap-2">

                <div className="bg-white rounded border
                    border-slate-300 shadow-xl w-[20%] max-h-[50%]">

                    <PlayerList players={players}/>
                </div>

                <div className="w-[50%]">
                    <GameField scene={scene}/>
                </div>

                <div className="w-[28%]">
                    <SessionEditor sendConnectionFunc={sendCode}/>
                </div>
            </div>
        </div>
    );
}