package org.soitoolkit.tools.generator.maven;

import org.junit.Test;
import org.soitoolkit.tools.generator.model.enums.MuleVersionEnum;


public class GeneratorTests {

	@Test
	public void testEnum() {
		MuleVersionEnum e = MuleVersionEnum.MULE_3_1_2;
		System.out.println(e.name());

		try {
			System.out.println(MuleVersionEnum.get(0).name());
		} catch (Exception e1) {
			e1.printStackTrace();
		}

		try {
			System.out.println(MuleVersionEnum.valueOf("MULE_3_1_2").name());
		} catch (Exception e1) {
			e1.printStackTrace();
		}

		try {
			System.out.println(MuleVersionEnum.getByLabel("3.1.2").name());
		} catch (Exception e1) {
			e1.printStackTrace();
		}
	}
}