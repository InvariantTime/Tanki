import { Alert, AlertDescription, AlertIcon, AlertTitle, Button, Text, Textarea } from "@chakra-ui/react";
import { Editor } from "@monaco-editor/react";
import { useState } from "react";

const defaultCode = `
    function update()
        --loop here
    end

    function onDamaged()
        --code when tank under attack
    end
`;

interface Props {
    sendConnectionFunc: (code: string) => Promise<any>
}

const defaultCodeResult: CodeResult = { error: "", isSuccess: true };

export const SessionEditor = ({ sendConnectionFunc }: Props) => {
    const [code, setCode] = useState<string | undefined>(defaultCode);
    const [status, setStatus] = useState<CodeResult>(defaultCodeResult);

    const onSendClick = async () => {
        var result = await sendConnectionFunc(code ?? defaultCode);
        setStatus({ isSuccess: result.isSuccess, error: result.error });
    }

    const onEditorChanged = (code: string | undefined) => {
        if (status?.isSuccess !== true)
            setStatus(defaultCodeResult);

        setCode(code);
    }

    return (
        <div className="bg-white rounded border
            border-slate-300 shadow-xl h-full w-full">
            <Text as="b">Editor</Text>

            <div className="h-[70%]">
                <Editor theme="light"
                    value={code} onChange={onEditorChanged}
                    defaultValue={defaultCode} defaultLanguage="lua"
                    options={{ minimap: { enabled: false }, fontSize: 16 }} />
            </div>

            <div className="border-t p-2 border-slate-400 h-[20%]">
                <div className="mb-5">
                    <Alert status="error" visibility={"hidden"}>
                        <AlertIcon />
                        <AlertDescription>ERROR</AlertDescription>
                    </Alert>
                </div>
            </div>

            <div className="p-5 float-right">
                <Button colorScheme="green" onClick={onSendClick}>
                    Send
                </Button>
            </div>
        </div>
    );
}

type CodeResult =
    {
        isSuccess: boolean,
        error: string
    };