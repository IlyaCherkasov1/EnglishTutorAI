import {GenerateChatCompletion} from "@/app/api/textGeneration/textGenerationApi";
import {FormEvent, useState} from "react";

interface Props {
    setCorrection: (value: string) => void;
}

const UserInput = (props: Props) => {
    const [sentence, setSentence] = useState ( '' );

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        var response = await GenerateChatCompletion(sentence);
        props.setCorrection(response)
    };

    return (
        <form id="user-input" onSubmit={handleSubmit}>
            <label htmlFor="sentence-input">Enter the text:</label>
            <textarea
                id="sentence-input"
                onChange={( e ) => setSentence(e.target.value)}/>
            <button id="submit-btn">Submit</button>
        </form>
    );
};

export default UserInput;
